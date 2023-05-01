﻿using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ZiplineClient
{
    public partial class MainForm : Form
    {
        #region Form
        //------------------------------------PRIVATE VARIABLES------------------------------------//
        record FileData(string FileGUID, string Username, string Filename, long FileSize);
        List<FileData> userFiles = default!;
        readonly string username = default!;
        readonly string current_ip = default!;
        //[RestrictAccessTo(nameof(NewFileSelectButton_Click), nameof(AddNewFile))]
        string original_filename = ""; // For use with the add new file section. Probably should rework this. 
        string requested_file = ""; // For use with downloading file. Should also rework this.
        //-----------------------------------------------------------------------------------------//
        public MainForm()
        {
            InitializeComponent();

            this.Enabled = false;
            LoginForm lf = new();
            this.Show();
            lf.ShowDialog();
            if (lf.UserAuthenticated)
            {
                username = lf.Username;
                current_ip = lf.CurrentIP;
                
                GetUsersAndFiles();
                userFiles = LoadUserFileList();
                VerifyUserFiles();
                Task.Run(() => ServerListener());

                this.Text = "Zipline Client - " + username;
                this.Enabled = true;
            }
            else { Application.Exit(); }
        }
        #endregion

        #region MiscEvents
        private void CentralTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            mfUsersToShareList.Items.Clear();
            mfUsersSharedWithList.Items.Clear();

            mfAcceptFileButton.Enabled = mfNewFileFilenameTextBox.Enabled =
                    mfShareButton.Enabled = mfUnshareButton.Enabled = false;
            mfNewFileFilenameTextBox.Text = "";
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
                _ = ServerCommunicator.SendCommandToServer(outgoing_payload); // Have nothing to do with the result right now. 
            }

            if (userFiles.Any())
            {
                string json = JsonSerializer.Serialize(userFiles);
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                string encoded = Convert.ToBase64String(bytes);
                string path = Application.StartupPath + @"config.bin";
                if (!File.Exists(path)) { var f = File.Create(path); f.Dispose(); }

                using var writer = new BinaryWriter(File.Open(path, FileMode.Truncate, FileAccess.Write));
                writer.Write(encoded);
            }

            ServerConnection.Instance.Socket.Close();
        }
        #endregion
    }
}
