using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using static ZiplineClient.MainForm;

namespace ZiplineClient
{
    public partial class MainForm : Form
    {
        #region Form
        //------------------------------------PRIVATE VARIABLES------------------------------------//
        public record FileData(string FileGUID, string Username, string Filename, long FileSize);
        ConfigData userConfig = default!;
        readonly string username = default!;
        string original_filename = ""; // For use with the add new file section. Probably should rework this. 
        string requestedFile = ""; // For use with downloading file. Should also rework this.
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
                userConfig = lf.UserConfig;
                GetUsersAndFiles(true);
                VerifyUserFiles();
                Task.Run(() => ServerListener());

                this.Text = "Zipline Client - " + username;
                this.Enabled = true;

                if (userConfig.Files is not null)
                {
                    foreach (FileData file in userConfig.Files) // Populate myfiles grid.
                    { mfMyFilesDataGrid.Rows.Add(file.FileGUID, file.Filename, file.FileSize); }
                    mfMyFilesDataGrid.Refresh();
                } else { Console.WriteLine("Files was null."); }
                
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
            if (e.CloseReason == CloseReason.UserClosing) // Only logs out if the user manually exits. 
            {
                string json = JsonSerializer.Serialize(userConfig);
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                string encoded = Convert.ToBase64String(bytes);
                string path = Application.StartupPath + "/Users/" + username + ".config";
                if (!File.Exists(path)) { var f = File.Create(path); f.Dispose(); }

                using var writer = new BinaryWriter(File.Open(path, FileMode.Truncate, FileAccess.Write));
                writer.Write(encoded);

                var thread = new Thread(() =>
                {
                    var outgoing_payload = new
                    {
                        Command = "logout_user",
                        Username = username,
                    };
                    Console.WriteLine("Logging out.");
                    ServerCommunicator.SendCommandToServer(outgoing_payload);
                });
                thread.Start();

                var timeout = TimeSpan.FromSeconds(5);
                var stopwatch = Stopwatch.StartNew();
                while (stopwatch.Elapsed < timeout)
                {
                    if (ServerConnection.Instance.Socket.Poll(1000, SelectMode.SelectRead) &&
                        ServerConnection.Instance.Socket.Available == 0)
                    { break; } // Server connection is closed. 
                }
            }

            ServerConnection.Instance.Socket.Close();
        }

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            if (mfNewPasswordTextBox.Text == mfRetypePasswordTextBox.Text)
            {
                string password = ServerCommunicator.HashPassword(
                    mfNewPasswordTextBox.Text, userConfig, nameof(userConfig.Salt), true);
                var outgoing_payload = new
                {
                    Command = "change_password",
                    Username = username,
                    Password = password,
                };

                string server_response = ServerCommunicator.SendCommandToServer(outgoing_payload);
                if (server_response.Contains("OK"))
                {
                    MessageBox.Show("Password changed.");
                    mfNewPasswordTextBox.Text = mfRetypePasswordTextBox.Text = string.Empty;
                } else
                {
                    string msg = "There was an unexpected problem changing your password, please try again.";
                    MessageBox.Show(msg, "Password Change Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"Encountered a problem trying to change the user's password. Server response: {server_response}");
                }

            } else
            { MessageBox.Show("Passwords do not match."); }
        }
        #endregion
    }

    public struct ConfigData
    {
        // A lot of redundancy in this struct is due to fuckery with initializing lists to be empty, not null. 
        private List<FileData> fileData;
        private List<FileData> favorites;
        private byte[] salt;
        public List<FileData> Files 
        { 
            get 
            { 
                fileData ??= new List<FileData>();
                return fileData; 
            }
            set { fileData = value ?? new List<FileData>(); } 
        }
        public List<FileData> Favorites
        {
            get 
            { 
                favorites ??= new List<FileData>();
                return favorites; 
            }
            set { favorites = value ?? new List<FileData>(); }
        }
        public byte[] Salt 
        { 
            get { return salt; } 
            set { salt = value; }
        }
        public ConfigData(List<FileData> _fileData, List<FileData> _favorites, byte[] _salt) : this()
        {
            this.fileData = _fileData ?? new List<FileData>();
            this.favorites = _favorites ?? new List<FileData>();
            this.salt = _salt;
        } 
    }
}
