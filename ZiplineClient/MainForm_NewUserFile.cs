using System.Diagnostics.Contracts;
using System.Drawing;

namespace ZiplineClient
{
    public partial class MainForm : Form
    {
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

                GetUsersAndFiles();
                mfUsersToShareList.Items.Clear();
                mfUsersSharedWithList.Items.Clear();
                foreach (var item in mfOnlineUsersList.Items) { mfUsersToShareList.Items.Add(item); }
            }
        }

        private void NewFileAcceptButton_Click(object sender, EventArgs e)
        { AddNewFile(true, mfNewFileFilenameTextBox.Text); }

        private async void AddNewFile(bool viaMyFilesTab, string newFilename)
        {
            if(!viaMyFilesTab) 
            { 
                original_filename = Application.StartupPath + "/Shared Files/" + newFilename; 
                newFilename = Path.GetFileNameWithoutExtension(original_filename);
            }

            // Check file size.
            FileInfo info = new(original_filename);
            long fileSize = info.Length / 1024; // Length in kb
            if (fileSize > 51200)
            {
                string msg = "There is a filesize limit of 50mb. Please select a different file.";
                MessageBox.Show(msg, "File Add Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prepare to copy file to shared folder.
            string full_filename = newFilename.Replace(' ', '_') + Path.GetExtension(original_filename);
            string directory = Application.StartupPath + "/Shared Files/";
            string destination = Path.Combine(directory, full_filename);
            bool overwrite_file = false;
            if (File.Exists(destination))
            {
                string msg = "A file with the same filename already exists in your shared folder. " +
                    "If you overwrite it, the original file will be deleted from the server if applicable, overwrite?";
                var result = MessageBox.Show(msg, "Share File Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.No) { return; }

                var file_to_delete = userFiles.Find(f => f.Filename == full_filename);
                if (file_to_delete is not null) { DeleteFile(file_to_delete.FileGUID); }
                overwrite_file = true;
            }

            // Inform server of new file.
            string file_guid = Guid.NewGuid().ToString();
            string shared_users = "";
            if (viaMyFilesTab)
            {
                foreach (var item in mfUsersSharedWithList.Items) { shared_users += item.ToString() + "?"; }
                if (shared_users.Length > 0) { shared_users = shared_users[..^1]; }
            }

            var outgoing_payload = new
            {
                Command = "add_new_file",
                Username = username,
                FileGUID = file_guid,
                Filename = full_filename,
                FileSize = fileSize,
                AuthorizedUsers = shared_users
            };

            string server_response = await Program.SendCommandToServerAsync(outgoing_payload);
            string operationResult;
            switch (server_response)
            {
                case "STATUS_FAILURE":
                    operationResult = "There was a problem adding a new file with unknown details.";
                    break;
                case "STATUS_TIMEOUT":
                    operationResult = "There was a problem adding a new file: Server timeout.";
                    break;
                case "STATUS_FILE_EXISTS":
                    operationResult = "There was a problem adding a new file: File already exists.";
                    break;
                case "STATUS_OK":
                    // Copy file. Encryption goes here. 
                    if (overwrite_file)
                    { // Bootleg workaround to file in use problem. 
                        File.Copy(original_filename, destination + "2");
                        File.Delete(destination);
                        File.Move(destination + "2", destination);
                    }
                    else { File.Copy(original_filename, destination); }

                    // Update personal table.
                    mfMyFilesDataGrid.Rows.Add(file_guid, full_filename, fileSize);
                    userFiles.Add(new FileData(file_guid, username, full_filename, fileSize));
                    operationResult = "The file was successfully added to the server list.";
                    break;
                default: 
                    operationResult = "There was a problem adding a new file: There was an unexpected response from the server.";
                    break; // Unexpected response from server. LOG THIS IN THE FUTURE
            }
            if (operationResult.Contains("Problem")) 
            { MessageBox.Show(operationResult, "File Add Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else { MessageBox.Show(operationResult, "File Add Success", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
    }
}