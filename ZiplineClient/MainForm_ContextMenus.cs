using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace ZiplineClient
{
    public partial class MainForm : Form
    {
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

        private async void DownloadFile(DataGridView datagrid)
        {
            int idx = datagrid.CurrentCell.RowIndex;
            string target_guid = (string)datagrid.Rows[idx].Cells[0].Value;
            string target_user = (string)datagrid.Rows[idx].Cells[1].Value;
            requested_file = (string)datagrid.Rows[idx].Cells[2].Value;

            var outgoing_payload = new
            {
                Command = "download_file",
                Username = username,
                TargetUser = target_user,
                TargetGUID = target_guid
            };

            SendPayloadNoResponse(outgoing_payload);
        }

        private void UsersContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        { GetUsersAndFiles(); }

        private void PersonalFileContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int idx = mfMyFilesDataGrid.CurrentCell.RowIndex;
            if (idx == -1) { return; }
#nullable disable
            string guid = mfMyFilesDataGrid.Rows[idx].Cells[0].Value.ToString();
            DeleteFile(guid);
#nullable enable
        }

        private async void DeleteFile(string guid)
        {
            var outgoing_payload = new
            {
                Command = "delete_file",
                FileGUID = guid,
            };

            string server_response = ServerCommunicator.SendCommandToServer(outgoing_payload);
            if (server_response.Contains("OK"))
            {
                var idx = userFiles.FindIndex(x => x.FileGUID == guid);
                if (idx != -1) 
                {
                    string filename = Application.StartupPath + "/Shared Files/" + userFiles[idx].Filename;
                    if (File.Exists(filename)) { File.Delete(filename); }
                    userFiles.RemoveAt(idx); 
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