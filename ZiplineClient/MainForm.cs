using System.Data.Common;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZiplineClient
{

    public partial class MainForm : Form
    {
        private record FileListRow(string GUID, string Username, string Filename, string Filesize, bool FileAccess);

        readonly byte[] header_bytes = { 0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D };
        readonly IPEndPoint server_endpoint = new(IPAddress.Parse("137.184.241.2"), 52525);
        readonly string username;
        readonly byte[] username_bytes; // In ASCII decimal format
        string original_filename = ""; // For use with the add new file section. Probably should rework this. 
        public MainForm(string _username)
        {
            InitializeComponent();
            username = _username;
            username_bytes = new byte[20];
            for (int i = 0; i < username.Length; ++i) { username_bytes[i] = (byte)username[i]; }
            for (int j = username.Length; j < 20; ++j) { username_bytes[j] = 0; }

            this.Text = "Zipline Client - " + username;
        }

        private void GetUsersAndFiles()
        { // Method queries server for a list of the online users and shared files. 
            string command = "get_users_files";
            byte[] cmd_bytes = Encoding.UTF8.GetBytes(command);
            int pkg_length = 112 + cmd_bytes.Length;
            byte[] length_bytes = BitConverter.GetBytes(pkg_length);
            byte[] outgoing_package = new byte[pkg_length];

            length_bytes.CopyTo(outgoing_package, 0);
            header_bytes.CopyTo(outgoing_package, 4);
            username_bytes.CopyTo(outgoing_package, 12);
            cmd_bytes.CopyTo(outgoing_package, 32);

            TcpClient tclient = new();
            tclient.Connect(server_endpoint);
            try
            {
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

                // testing
                string json = Encoding.UTF8.GetString(incoming_package);
                MessageBox.Show(json);
                return;
                List<FileListRow>? fileListRows = JsonSerializer.Deserialize<List<FileListRow>>(json);
                // end testing
                if (fileListRows != null)
                {
                    foreach (FileListRow row in fileListRows)
                    {
                        mfMainDataGrid.Rows.Add(row.GUID, row.Username, row.Filename, row.Filesize, row.GUID, row.FileAccess);
                        mfOnlineUsersList.Items.Add(row.Username);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = $"Unable to communicate with the server.\nInfo: {ex.Message}";
                var result = MessageBox.Show(msg, "Network error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Retry) { GetUsersAndFiles(); } else { this.Close(); }
            }
            finally { tclient.Close(); }
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
    }
}
