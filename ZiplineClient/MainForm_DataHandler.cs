using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ZiplineClient
{
    public partial class MainForm : Form
    {
        private async void ServerListener()
        {
            while (true)
            {
                lock (ServerConnection.Instance.streamLock)
                {
                    try
                    {
                        if (ServerConnection.Instance.Socket.Available > 0)
                        {
                            // Get size data
                            byte[] size_data = new byte[4];
                            int bytesRead = 0, totalBytesRead = 0;
                            do
                            {
                                bytesRead = ServerConnection.Instance.Socket.Receive(
                                    size_data, totalBytesRead, size_data.Length - totalBytesRead, SocketFlags.None);
                                totalBytesRead += bytesRead;
                            } while (totalBytesRead < 4);
                            if (BitConverter.IsLittleEndian) { Array.Reverse(size_data); }
                            int package_length = BitConverter.ToInt32(size_data, 0) - 12;

                            // Check on size data
                            const int MAX_DATA_SIZE = 52429000; // 50mb in bytes + 200bytes flex room.
                            if (package_length > MAX_DATA_SIZE) 
                            {
                                Console.WriteLine($"Detected a package that was too large ({MAX_DATA_SIZE} bytes)");
                                FlushBadData(); 
                                return; 
                            }

                            // Get header
                            byte[] header_data = new byte[8];
                            totalBytesRead = 0;
                            do
                            {
                                try
                                {
                                    bytesRead = ServerConnection.Instance.Socket.Receive(
                                    header_data, totalBytesRead, header_data.Length - totalBytesRead, SocketFlags.None);
                                    totalBytesRead += bytesRead;
                                } catch { continue; }
                            } while (totalBytesRead < 8);

                            // Check on header data and switch on result.
                            int pkgType = VerifyExpectedHeader(header_data);
                            switch (pkgType)
                            {
                                case 1: // Command
                                    byte[] incoming_package = new byte[package_length];
                                    totalBytesRead = 0;
                                    do
                                    {
                                        bytesRead = ServerConnection.Instance.Socket.Receive(
                                            incoming_package, totalBytesRead, package_length - totalBytesRead, SocketFlags.None);
                                        totalBytesRead += bytesRead;
                                    } while (totalBytesRead < package_length);
                                    string json = Encoding.UTF8.GetString(incoming_package);
                                    HandleIncomingData(json);
                                    break;
                                case 2: // File
                                    Console.WriteLine("Received a file.");
                                    FileTransferForm download = new(package_length, requestedFile);
                                    if (download.ShowDialog() == DialogResult.OK)
                                    { MessageBox.Show("Download complete."); }
                                    break;
                                default: // Neither
                                    Console.WriteLine("Invalid header detected.");
                                    FlushBadData();
                                    break;
                            }
                        }
                    } catch (ObjectDisposedException)
                    {
                        Console.WriteLine("Tried to access disposed socket. If this prints on logout, all is fine.");
                    }
                }
                await Task.Delay(1);
            }
        }

        private void HandleIncomingData(string jsonPackage)
        {
            jsonPackage = jsonPackage.Replace("'", "\"");
            JsonDocument jdoc = JsonDocument.Parse(jsonPackage);
            JsonElement root = jdoc.RootElement;
            if (root.TryGetProperty("Command", out JsonElement cmd))
            {
                Console.WriteLine($"Received command: {cmd.GetString()}");
                switch (cmd.GetString())
                {
                    case "download_file":
                        string requesting_user = root.GetProperty("Username").GetString() ?? "ERR_BAD_USERNAME";
                        string requested_guid = root.GetProperty("TargetGUID").GetString() ?? "ERR_BAD_GUID";
                        string filename = userConfig.Files.FirstOrDefault(f => f.FileGUID == requested_guid)?.Filename ?? "NO_MATCH";
                        if (filename is not "NO_MATCH")
                        {
                            string request = requesting_user + " would like to download " + filename;
                            var result = MessageBox.Show(request, "File Download Request", 
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if(result == DialogResult.No) 
                            {
                                var outgoing_payload = new
                                {
                                    Command = "send_file",
                                    Response = "STATUS_FAILURE",
                                    TargetUser = requesting_user
                                };
                                ServerCommunicator.SendCommandToServer(outgoing_payload);
                                return;
                            } 
                        }
                        FileTransferForm upload = new(filename, requesting_user);
                        if (upload.ShowDialog() == DialogResult.OK)
                        {
                            MessageBox.Show("Upload Complete.");
                        }
                        break;
                    case "send_file":
                        string response = root.GetProperty("Response").GetString() ?? "STATUS_FAILURE";
                        if (response == "STATUS_FAILURE")
                        {
                            string msg = "Your request to download " + requestedFile + " was rejected.";
                            MessageBox.Show(msg, "File Request Denied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        } else
                        { Console.WriteLine($"Detected an unexpected response for downloading a file: {response}"); }
                        break;
                    default:
                        Console.WriteLine("Detected invalid command.");
                        break;
                }
            }
        }

        private static void FlushBadData()
        {
            byte[] discard_buffer = new byte[1024];
            while (ServerConnection.Instance.Socket.Available > 0)
            { ServerConnection.Instance.Socket.Receive(discard_buffer, SocketFlags.None); }
            Console.WriteLine("Discarded faulty or bad data.");
        }

        private static int VerifyExpectedHeader(byte[] header_bytes)
        {
            byte[] expected_header_command = { 0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D };
            byte[] expected_header_file = { 0x7F, 0x52, 0x8B, 0x59, 0xE9, 0xF9, 0x04, 0xC3 };
            bool isCommand = true, isFile = true;
            for (int i = 0; i < 8; ++i)
            {
                if (header_bytes[i] != expected_header_command[i]) { isCommand = false; }
                if (header_bytes[i] != expected_header_file[i]) { isFile = false; }
            }
            if (isCommand) { return 1; }
            if (isFile) { return 2; }
            return 0; // Is neither.
        }
    }
}