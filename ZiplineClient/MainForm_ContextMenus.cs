namespace ZiplineClient
{
    public partial class MainForm : Form
    {
        private void FileContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuStrip cmstrip = (ContextMenuStrip)sender;
            DataGridView dgv = (DataGridView)cmstrip.SourceControl;
            int idx = dgv.CurrentCell.RowIndex;
            if (dgv.Rows.Count == 0 || idx == -1) { return; } // Sidesteps an exception from trying to download nothing.

            switch(e.ClickedItem.Text)
            {
                case "Download": 
                    DownloadFile_Click(dgv); 
                    break;
                case "Request Access":
                    Console.WriteLine("Access sharing not implemented, all files are shared by default.");
                    break;
                case "Favorite/Unfavorite":
                    string guid = (string)dgv.Rows[idx].Cells[0].Value;
                    if (dgv.Name.Contains("Main"))
                    {
                        foreach (DataGridViewRow row in mfFavoritesDataGrid.Rows) // Check if favorites contains the file.
                        { if ((string)row.Cells[0].Value == guid) { return; } }

                        FileData new_fav = new(
                            guid,
                            (string)dgv.Rows[idx].Cells[1].Value,
                            (string)dgv.Rows[idx].Cells[2].Value,
                            (long)dgv.Rows[idx].Cells[3].Value
                            );

                        mfFavoritesDataGrid.Rows.Add(
                            new_fav.FileGUID,
                            new_fav.Username,
                            new_fav.Filename,
                            new_fav.FileSize.ToString(),
                            true
                            );

                        userConfig.Favorites.Add(new_fav);
                    } else // Favorites
                    { 
                        mfFavoritesDataGrid.Rows.RemoveAt(idx);
                        userConfig.Favorites.RemoveAll(f => f.FileGUID == guid);
                    }
                    mfFavoritesDataGrid.Refresh();
                    break;
            }
        }

        private void DownloadFile_Click(DataGridView datagrid)
        {
            int idx = datagrid.CurrentCell.RowIndex;

            bool access = (bool)datagrid.Rows[idx].Cells[4].Value;
            if (!access) 
            {
                string msg = "Unable to download the file. Either you lack access or the owner is offline.";
                MessageBox.Show(msg, "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            string target_guid = (string)datagrid.Rows[idx].Cells[0].Value;
            string target_user = (string)datagrid.Rows[idx].Cells[1].Value;
            requestedFile = (string)datagrid.Rows[idx].Cells[2].Value;

            var outgoing_payload = new
            {
                Command = "download_file",
                Username = username,
                TargetUser = target_user,
                TargetGUID = target_guid
            };

            Console.WriteLine($"Requesting to download file with GUID: {target_guid}");
            ServerCommunicator.SendCommandToServer(outgoing_payload);
        }

        private void UsersContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        { GetUsersAndFiles(); }

        private void PersonalFileContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (mfMyFilesDataGrid.CurrentCell == null) { return; }
            int idx = mfMyFilesDataGrid.CurrentCell.RowIndex;
#nullable disable
            string guid = (string)mfMyFilesDataGrid.Rows[idx].Cells[0].Value;
            DeleteFile(guid);
            mfMyFilesDataGrid.Rows.RemoveAt(idx);
#nullable enable
        }

        private void DeleteFile(string guid)
        {
            var outgoing_payload = new
            {
                Command = "delete_file",
                FileGUID = guid,
            };

            Console.WriteLine($"Informing server that file with GUID {guid} is being deleted.");
            string server_response = ServerCommunicator.SendCommandToServer(outgoing_payload);
            if (server_response.Contains("OK"))
            {
                Console.WriteLine("Deleting file...");
                var idx = userConfig.Files.FindIndex(x => x.FileGUID == guid);
                if (idx != -1) 
                {
                    string filename = Application.StartupPath + "/Shared Files/" + userConfig.Files[idx].Filename;
                    if (File.Exists(filename)) { File.Delete(filename); }
                    userConfig.Files.RemoveAt(idx); 
                }
                string msg = "File successfully deleted.";
                MessageBox.Show(msg, "Delete success.", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } else
            {
                string messg = "Unable to delete file. Try again later.";
                MessageBox.Show(messg, "File Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}