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
            string source_user = (string)datagrid.Rows[idx].Cells[1].Value;
            requested_file = (string)datagrid.Rows[idx].Cells[2].Value;

            var outgoing_payload = new
            {
                Command = "get_user_ip",
                TargetUser = source_user,
            };

            string server_response = await Program.SendCommandToServerAsync(outgoing_payload);

            switch (server_response)
            {
                case "STATUS_FAILURE":
                    string msg = $"Could not get valid data from server.";
                    var result = MessageBox.Show(msg, "Network error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Retry) { GetUsersAndFiles(); } else { this.Close(); }
                    break;
                case "STATUS_TIMEOUT":
                    string foo = $"Could not communicate with server: Network timeout.";
                    var res = MessageBox.Show(foo, "Network error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (res == DialogResult.Retry) { GetUsersAndFiles(); } else { this.Close(); }
                    break;
                default:
                    int ndx = server_response.IndexOf(':') + 2;
                    string[] user_ip = server_response[ndx..^5].Split(':');
                    var rtr_outgoing_payload = new
                    {
                        Command = "download_file",
                        Username = username,
                        CurrentIP = current_ip,
                        TargetGUID = target_guid
                    };
                    Program.SendCommandToUser(rtr_outgoing_payload, user_ip[0], user_ip[1]);
                    break;
            }
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

            string server_response = await Program.SendCommandToServerAsync(outgoing_payload);
        }
    }
}