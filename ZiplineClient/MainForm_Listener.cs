using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ZiplineClient
{
    public partial class MainForm : Form
    {
        private async void ConnectionListener(int port)
        {
            try
            {
                TcpListener listener = new(IPAddress.Any, port);
                listener.Start();

                while (true)
                {
                    await Task.Run(() => listener.AcceptTcpClientAsync());
                }
            }
            catch (Exception ex)
            {
                if (ex is SocketException exception && exception.ErrorCode == 10013) // ERROR_ACCESS_DENIED
                {
                    // Display a message box prompting the user to restart the application in administrator mode.
                    string msg = "Failed to open port for communication. Restart Zipline in administrator mode?";
                    var result = MessageBox.Show(msg, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (result == DialogResult.Yes)
                    {
                        Application.Exit();
                        // Start a new instance of the application with administrative privileges
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = Application.ExecutablePath,
                            UseShellExecute = true,
                            Verb = "runas" // Run as administrator
                        });
                    }
                }
                else
                {
                    string msg = $"Unable to open a port for communication.\nInfo: {ex.Message}";
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void HandleConnection(TcpClient client)
        {
            NetworkStream ns = client.GetStream();

            int bytesRead = 0, totalBytesRead = 0;
            byte[] length_bytes = new byte[4];
            do
            {
                bytesRead = ns.Read(length_bytes, totalBytesRead, length_bytes.Length - totalBytesRead);
                totalBytesRead += bytesRead;
            } while (totalBytesRead < 4);
            int package_size = BitConverter.ToInt32(length_bytes, 0) - 12; // Minus header and package size data.

            totalBytesRead = 0;
            byte[] header_bytes = new byte[8];
            do
            {
                bytesRead = ns.Read(header_bytes, totalBytesRead, header_bytes.Length - totalBytesRead);
                totalBytesRead += bytesRead;
            } while (totalBytesRead < 8);

            switch (VerifyExpectedHeader(header_bytes))
            {
                case 0: // Is neither a command nor file.
                    // Do nothing for now. May log an invalid connection attempt. 
                    break;
                case 1: // Is a command.
                    totalBytesRead = 0;
                    byte[] package_bytes = new byte[8];
                    do
                    {
                        bytesRead = ns.Read(package_bytes, totalBytesRead, package_bytes.Length - totalBytesRead);
                        totalBytesRead += bytesRead;
                    } while (totalBytesRead < package_size);
                    string json = Encoding.UTF8.GetString(package_bytes);
                    dynamic obj = JsonSerializer.Deserialize<dynamic>(json);

                    switch (obj.Command)
                    {
                        case "download_file":
                            string path = Application.StartupPath + "/Shared Files/" + obj.Filename;
                            int lst_idx = obj.CurrentIP.LastIndexOf(":");
                            string ip = obj.CurrentIP.Substring(0, lst_idx);
                            string port = obj.CurrentIP.Substring(lst_idx + 1);



                            if (File.Exists(path))
                            {
                                string msg = obj.Username + " is requesting to download " + obj.Filename;
                                var res = MessageBox.Show(msg, "Download Request", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (res == DialogResult.Yes)
                                {
                                    if (SendFile(path, ip, port))
                                    {
                                        MessageBox.Show("File successfully send.", "Send Success",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    } else
                                    { // Future: include reason why it failed. 
                                        MessageBox.Show("The file was not sent.", "Send fail.",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                } else
                                {
                                    var response_package = new
                                    {
                                        Command = "download_rejected",
                                        Username = username
                                    };
                                    Program.SendCommandToUser(response_package, ip, port);
                                }
                            } else
                            {
                                var response_package = new
                                {
                                    Command = "download_404",
                                    Username = username
                                };
                                Program.SendCommandToUser(response_package, ip, port);
                            }
                            break;
                        case "download_rejected":
                            break;
                        case "download_404":
                            break;
                    }
                    break;
                case 2: // Is a file.
                    DownloadForm df = new(ns, package_size, requested_file);
                    var result = df.ShowDialog();
                    if (result == DialogResult.OK)
                    { MessageBox.Show("Download success.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } else
                    { MessageBox.Show("Download aborted.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    break;
            }
            client.Close();
        }

        private static int VerifyExpectedHeader(byte[] bytes)
        {
            byte[] expected_header_command = { 0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D };
            byte[] expected_header_file = { 0x7F, 0x52, 0x8B, 0x59, 0xE9, 0xF9, 0x04, 0xC3 };
            bool isCommand = true, isFile = true;
            for (int i = 0; i < 8; ++i)
            {
                if (bytes[i] != expected_header_command[i]) { isCommand = false; }
                if (bytes[i] != expected_header_file[i]) { isFile = false; }
            }
            if (isCommand) { return 1; }
            if (isFile) { return 2; }
            return 0; // Is neither.
        }

        private static bool SendFile(string filepath, string ip, string port)
        {
            FileStream fs = new(filepath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new(fs);

            byte[] file_bytes = br.ReadBytes((int)fs.Length);
            int pkg_length = 12 + file_bytes.Length;
            byte[] length_bytes = BitConverter.GetBytes(pkg_length);
            if (BitConverter.IsLittleEndian) { Array.Reverse(length_bytes); }
            byte[] outgoing_package = new byte[pkg_length];
            byte[] header_bytes = { 0x7F, 0x52, 0x8B, 0x59, 0xE9, 0xF9, 0x04, 0xC3 }; // Header for sending files. 

            length_bytes.CopyTo(outgoing_package, 0);
            header_bytes.CopyTo(outgoing_package, 4);
            file_bytes.CopyTo(outgoing_package, 12);

            TcpClient tclient = new();
            try
            {
                tclient.Connect(new(IPAddress.Parse(ip), int.Parse(port)));
                NetworkStream stream = tclient.GetStream();
                stream.Write(file_bytes, 0, file_bytes.Length);
                tclient.Close();
                return true;
            }
            catch (Exception ex)
            {
                string mesg = $"Unable to communicate with the other user.\nInfo{ex.Message}";
                MessageBox.Show(mesg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tclient.Close();
                return false;
            }
        }
    }
}