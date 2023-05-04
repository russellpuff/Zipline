using System.Text.Json;
using System.Text.RegularExpressions;

namespace ZiplineClient
{
    public partial class MainForm : Form
    {
        private void GetUsersAndFiles(bool firstTime = false)
        { // Method queries server for a list of the online users and shared files. 
            mfOnlineUsersList.Items.Clear();
            var outgoing_payload = new
            {
                Command = "get_users_files",
                Username = username,
            };

            Console.WriteLine("Querying server for online users and their files.");
            string server_response = ServerCommunicator.SendCommandToServer(outgoing_payload);
            switch (server_response)
            {
                case "STATUS_FAILURE":
                    string msg = $"Could not get file data from server.";
                    var result = MessageBox.Show(msg, "Network error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Retry) { GetUsersAndFiles(); } else { this.Close(); }
                    break;
                case "STATUS_TIMEOUT":
                    string foo = $"Could not communicate with server: Network timeout.";
                    var res = MessageBox.Show(foo, "Network error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (res == DialogResult.Retry) { GetUsersAndFiles(); } else { this.Close(); }
                    break;
                default:
                    Console.WriteLine("Data acquired, updating data grid.");
                    mfMainDataGrid.Rows.Clear();
                    string json = server_response[3..^4]; // Strip away python fuckery that made the json incompatible. 
#nullable disable
                    // Build other users files list
                    List<FileData> files = JsonSerializer.Deserialize<List<FileData>>(json);
                    foreach (var file in files)
                    {
                        mfMainDataGrid.Rows.Add(
                            file.FileGUID,
                            file.Username,
                            file.Filename,
                            file.FileSize,
                            true // replace with real access later
                            );
                        if (file.Username == username) { continue; }
                        if (!mfOnlineUsersList.Items.Contains(file.Username)) { mfOnlineUsersList.Items.Add(file.Username); }
                    }
                    mfMainDataGrid.Refresh();
#nullable enable
                    // Build user favorites list
                    if (firstTime)
                    {
                        if (userConfig.Favorites is not null)
                        {
                            foreach (FileData fav in userConfig.Favorites)
                            {
                                mfFavoritesDataGrid.Rows.Add(
                                    fav.FileGUID,
                                    fav.Username,
                                    fav.Filename,
                                    fav.FileSize,
                                    files.Contains(fav) // Deny access if user is offline.
                                );
                            }
                            mfFavoritesDataGrid.Refresh();
                        }
                        else { Console.WriteLine("Fuckery detected."); }
                    }
                    break;
            }
        }

        private void VerifyUserFiles()
        {
            string directory = Application.StartupPath + "/Shared Files";
            string[] files = Directory.GetFiles(directory);
            string allFiles = "";
            foreach (string file in files) { allFiles += Path.GetFileName(file) + "?"; }
            if (allFiles != "") { allFiles = allFiles[..^1]; }
            var outgoing_payload = new
            {
                Command = "verify_user_files",
                Username = username,
                FileList = allFiles
            };

            Console.WriteLine("Verifying user's physical files against the server's database.");
            string server_response = ServerCommunicator.SendCommandToServer(outgoing_payload);
            switch (server_response)
            {
                case "STATUS_FAILURE":
                    string msg = $"Could not get verification data from server.";
                    var result = MessageBox.Show(msg, "Network error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Retry) { VerifyUserFiles(); } else { this.Close(); }
                    break;
                case "STATUS_TIMEOUT":
                    string foo = $"Could not communicate with server: Network timeout.";
                    var res = MessageBox.Show(foo, "Network error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (res == DialogResult.Retry) { VerifyUserFiles(); } else { this.Close(); }
                    break;
                default:
                    // This json string is SUPER funky and System.Text hates it.
                    string[] chunks = Regex.Split(server_response[2..^2], @"}, {");

                    List<(string, string)> pairs = new();
                    foreach (string chunk in chunks)
                    {
                        string[] parts = chunk.Split(':');
                        pairs.Add((
                            parts[0].Trim('[', '\'', '\"', ' '), // Remove Junk
                            parts[1].Trim('\'', '\"', ' ')
                            ));
                    }

                    List<string> missing_files = new();
                    List<string> unknown_files = new();
                    foreach (var pair in pairs)
                    {
                        switch (pair.Item2)
                        {
                            case "STATUS_OK": continue;
                            case "STATUS_MISSING_FILE": missing_files.Add(pair.Item1); break;
                            case "STATUS_UNKNOWN_FILE": unknown_files.Add(pair.Item1); break;
                        }
                    }

                    missing_files.RemoveAll(x => x == "");
                    unknown_files.RemoveAll(y => y == "");

                    if (missing_files.Count > 0)
                    {
                        Console.WriteLine("Found files that the server knows but the user lacks.");
                        string mesg = "The following files are known by the server but missing from your computer.\n\n";
                        foreach (string file in missing_files) { mesg += file + "\n"; }
                        mesg += "\nWould you like to delete these from your shared files list?";
                        var bar = MessageBox.Show(mesg, "Missing Files", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (bar == DialogResult.Yes)
                        {
                            bool problems = false;
                            foreach(string file in missing_files)
                            {
                                var outgoing_package = new
                                {
                                    Command = "delete_file",
                                    Username = username,
                                    Filename = file
                                };
                                server_response = ServerCommunicator.SendCommandToServer(outgoing_package);
                                if (!server_response.Contains("OK")) { problems = true; continue; }
                                // Remove from user's list if they have it. 
                                var idx = userConfig.Files.FindIndex(x => x.Filename == file);
                                if (idx != -1) { userConfig.Files.RemoveAt(idx); }
                            }
                            if (problems)
                            {
                                string ysg = "There were issues tring to delete the files. Try again later.";
                                MessageBox.Show(ysg, "File Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            } else
                            {
                                msg = "Files deleted successfully.";
                                MessageBox.Show(msg, "File Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    if (unknown_files.Count > 0)
                    {
                        Console.WriteLine("Found files the user has but the server is unaware of.");
                        string mssg = "The following files are in your Shared Files folder but not listed on the server.\n\n";
                        foreach (string file in unknown_files) { mssg += file + "\n"; }
                        mssg += "\nWould you like to share these now? Note that doing so will not grant anyone access.";
                        var dresult = MessageBox.Show(mssg, "Unknown Files", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (dresult == DialogResult.Yes)
                        { foreach (string file in unknown_files) { AddNewFile(false, file); } }
                    }
                    break;
            }
            Console.WriteLine("Verification complete.");
        }
    }
}