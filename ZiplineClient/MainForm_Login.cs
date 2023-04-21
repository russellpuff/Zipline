using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ZiplineClient
{
    public partial class MainForm : Form
    {
        private async void GetUsersAndFiles()
        { // Method queries server for a list of the online users and shared files. 
            mfOnlineUsersList.Items.Clear();
            var outgoing_payload = new
            {
                Command = "get_users_files",
                Username = username,
            };

            string server_response = await Program.SendCommandToServerAsync(outgoing_payload);
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
                    mfMainDataGrid.Rows.Clear();
                    string json = server_response[3..^4]; // Strip away python fuckery that made the json incompatible. 
#nullable disable
                    List<FileData> files = JsonSerializer.Deserialize<List<FileData>>(json);
                    foreach (var file in files)
                    {
                        mfMainDataGrid.Rows.Add(
                            file.FileGUID,
                            file.Username,
                            file.Filename,
                            file.FileSize,
                            false // replace with real access later
                            );
                        if (file.Username == username) { continue; }
                        if (!mfOnlineUsersList.Items.Contains(file.Username)) { mfOnlineUsersList.Items.Add(file.Username); }
                    }
                    mfMainDataGrid.Refresh();
#nullable enable
                    break;
            }
        }

        private async void VerifyUserFiles()
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

            string server_response = await Program.SendCommandToServerAsync(outgoing_payload);
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
                                server_response = await Program.SendCommandToServerAsync(outgoing_package);
                                if (!server_response.Contains("OK")) { problems = true; continue; }
                                // Remove from user's list if they have it. 
                                var idx = userFiles.FindIndex(x => x.Filename == file);
                                if (idx != -1) { userFiles.RemoveAt(idx); }
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
                        string mssg = "The following files are in your Shared Files folder but not listed on the server.\n\n";
                        foreach (string file in unknown_files) { mssg += file + "\n"; }
                        mssg += "\nWould you like to share these now? Note that doing so will not grant anyone access.";
                        var dresult = MessageBox.Show(mssg, "Unknown Files", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (dresult == DialogResult.Yes)
                        { foreach (string file in unknown_files) { AddNewFile(false, file); } }
                    }

                    break;
            }
        }

        private List<FileData> LoadUserFileList()
        {
            string path = Application.StartupPath + @"config.bin";
            if (!File.Exists(path)) 
            {
                string msg = "config.bin is missing, without it the user's filelist cannot be rebuilt. " +
                    "Try to query the server for lost data?";
                var result = MessageBox.Show(msg, "Missing config.bin", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    // implement this
                } else { return new(); }
            }

            using var reader = new BinaryReader(File.OpenRead(path));
            string encoded = reader.ReadString();
            byte[] bytes = Convert.FromBase64String(encoded);
            string json = Encoding.UTF8.GetString(bytes);

            List<FileData> files = JsonSerializer.Deserialize<List<FileData>>(json) ?? new();
            foreach(FileData file in files)
            { mfMyFilesDataGrid.Rows.Add(file.FileGUID, file.Filename, file.FileSize); }
            mfMyFilesDataGrid.Refresh();
            return files;
        }
    }
}