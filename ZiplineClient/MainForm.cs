using System.Data.Common;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ZiplineClient
{

    public partial class MainForm : Form
    {
        private record FileListRow(string GUID, string Username, string Filename, string Filesize, bool FileAccess);

        readonly string? username;
        readonly string? current_ip;
        string original_filename = ""; // For use with the add new file section. Probably should rework this. 
        public MainForm()
        {
            InitializeComponent();
            LoginForm lf = new();
            this.Show();
            lf.ShowDialog();
            if (lf.UserAuthenticated)
            {
                username = lf.Username;
                current_ip = lf.CurrentIP;
                _ = Task.Run(() => ConnectionListener(57321));
                this.Text = "Zipline Client - " + username;
            }
            else { this.Close(); }
        }

        private async void ConnectionListener(int port)
        {
            try
            {
                TcpListener listener = new(IPAddress.Any, port);
                listener.Start();

                while (true)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    _ = Task.Run(() => HandleConnection(client));
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
            if (VerifyExpectedHeader(header_bytes))
            {

            }
            else
            {

            }

        }

        private bool VerifyExpectedHeader(byte[] bytes)
        {
            byte[] expected_header = { 0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D };
            return false;
        }

        private void GetUsersAndFiles()
        { // Method queries server for a list of the online users and shared files. 
            var outgoing_payload = new
            {
                Command = "get_users_files",
                Username = username,
            };

            string server_response = Program.SendCommandToServer(outgoing_payload);
            if (server_response.Contains("OK"))
            {




                // Update users and files. 






            }
            else if (server_response is not "STATUS_FAILURE")
            { MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                string msg = $"Unable to communicate with the server.";
                var result = MessageBox.Show(msg, "Network error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Retry) { GetUsersAndFiles(); } else { this.Close(); }
            }
        }



        #region AddingNewFile
        private void NewFileSelectButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new()
            {
                InitialDirectory = "c:\\",
                FilterIndex = 0,
                RestoreDirectory = true
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                original_filename = open.FileName;
                mfNewFileFilenameTextBox.Text = Path.GetFileNameWithoutExtension(open.FileName);
                mfAcceptFileButton.Enabled = mfNewFileFilenameTextBox.Enabled =
                    mfShareButton.Enabled = mfUnshareButton.Enabled = true;

                foreach (var item in mfOnlineUsersList.Items) { mfUsersToShareList.Items.Add(item); }
            }
        }

        private void NewFileAcceptButton_Click(object sender, EventArgs e)
        { // PLACEHOLDER, REPLACE WITH ENCRYPTION
            string full_filename = mfNewFileFilenameTextBox.Text.Replace(' ', '_') + Path.GetExtension(original_filename);
            string directory = Application.StartupPath + "/Shared Files/";
            string destination = Path.Combine(directory, full_filename);
            File.Copy(original_filename, destination);
        }

        private void NewFileShareButton_Click(object sender, EventArgs e)
        {
            if (mfUsersToShareList.SelectedIndex == -1) { return; }
            mfUsersSharedWithList.Items.Add(mfUsersToShareList.Items[mfUsersToShareList.SelectedIndex]);
            mfUsersToShareList.Items.RemoveAt(mfUsersToShareList.SelectedIndex);
        }

        private void NewFileUnshareButton_Click(object sender, EventArgs e)
        {
            if (mfUsersSharedWithList.SelectedIndex == -1) { return; }
            mfUsersToShareList.Items.Add(mfUsersSharedWithList.Items[mfUsersSharedWithList.SelectedIndex]);
            mfUsersSharedWithList.Items.RemoveAt(mfUsersSharedWithList.SelectedIndex);
        }
        #endregion

        private void TEST_CLICK(object sender, EventArgs e)
        {
            GetUsersAndFiles();
        }

        private void CentralTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            mfAcceptFileButton.Enabled = mfNewFileFilenameTextBox.Enabled =
                    mfShareButton.Enabled = mfUnshareButton.Enabled = false;
            mfNewFileFilenameTextBox.Text = "";
        }

        private void FileContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuStrip cmstrip = (ContextMenuStrip)sender;
            if (e.ClickedItem.Text == "Download")
            { DownloadFile((DataGridView)cmstrip.SourceControl); }
            else // Item text is "Request access"
            {
                // Not implemented.
            }
        }

        private void DownloadFile(DataGridView datagrid)
        {
            int idx = datagrid.CurrentCell.RowIndex;
            string target_guid = (string)datagrid.Rows[idx].Cells[0].Value;
            string source_user = (string)datagrid.Rows[idx].Cells[1].Value;

            var outgoing_payload = new
            {
                Command = "get_user_ip",
                TargetUser = source_user,
            };

            string user_ip = Program.SendCommandToServer(outgoing_payload);
            if (user_ip is not "STATUS_FAILURE")
            {
                string[] ip = user_ip.Split(':');
                var rtr_outgoing_payload = new
                {
                    Command = "download_file",
                    Username = username,
                    CurrentIP = current_ip,
                    TargetGUID = target_guid
                };

                string json = JsonSerializer.Serialize(rtr_outgoing_payload);
                byte[] payload_bytes = Encoding.UTF8.GetBytes(json);
                int pkg_length = 12 + payload_bytes.Length; // Length 4 bytes + header 8 bytes + payload
                byte[] length_bytes = BitConverter.GetBytes(pkg_length);
                if (BitConverter.IsLittleEndian) { Array.Reverse(length_bytes); }
                byte[] outgoing_package = new byte[pkg_length];
                byte[] header_bytes = { 0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D };

                length_bytes.CopyTo(outgoing_package, 0);
                header_bytes.CopyTo(outgoing_package, 4);
                payload_bytes.CopyTo(outgoing_package, 12);

                TcpClient tclient = new();
                try
                {
                    tclient.Connect(new(IPAddress.Parse(ip[0]), int.Parse(ip[1])));
                    NetworkStream stream = tclient.GetStream();
                    stream.Write(outgoing_package, 0, outgoing_package.Length);

                    byte[] size_data = new byte[4];
                    int bytesRead = 0, totalBytesRead = 0;
                    do
                    {
                        bytesRead = stream.Read(size_data, totalBytesRead, size_data.Length - totalBytesRead);
                        totalBytesRead += bytesRead;
                    } while (totalBytesRead < 4);
                    if (BitConverter.IsLittleEndian) { Array.Reverse(size_data); }
                    int package_length = BitConverter.ToInt32(size_data, 0);

                    byte[] incoming_package = new byte[package_length];
                    totalBytesRead = 0;
                    do
                    {
                        bytesRead = stream.Read(incoming_package, totalBytesRead, package_length - totalBytesRead);
                        totalBytesRead += bytesRead;
                    } while (totalBytesRead < package_length);

                    string response = Encoding.UTF8.GetString(incoming_package);
                }
                catch (Exception ex)
                {
                    string msg = $"Unable to communicate with the other user.\nInfo{ex.Message}";
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { tclient.Close(); }
            }
            else
            {
                string msg = "Could not get target user's IP. Either you're not connected to the internet" +
                    "or they're not online.";
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var outgoing_payload = new
                {
                    Command = "logout_user",
                    Username = username,
                };
                _ = Program.SendCommandToServer(outgoing_payload); // Have nothing to do with the result. 
            }
        }
    }
}
