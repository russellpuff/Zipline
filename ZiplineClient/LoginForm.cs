using System.Media;
using System.Text.RegularExpressions;
using static ZiplineClient.MainForm;
using System.Text.Json;
using System.Text;

namespace ZiplineClient
{
    public partial class LoginForm : Form
    {
        public ConfigData UserConfig { get; private set; }
        public bool UserAuthenticated { get; private set; }
        public string Username { get; private set; }
        private bool rehashing;

        public LoginForm()
        {
            InitializeComponent();
            UserAuthenticated = false;
            Username = string.Empty;
            UserConfig = default!;
            rehashing = false;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            lfLoginButton.Enabled = false;
            if (!rehashing) { UserConfig = LoadUserConfig(); }
            string password = ServerCommunicator.HashPassword(lfPasswordTextBox.Text, UserConfig, nameof(UserConfig.Salt));
            var outgoing_payload = new
            {
                Command = "login_user",
                Username = lfUsernameTextBox.Text,
                Password = password,
            };
            Console.WriteLine($"Logging in user {lfUsernameTextBox.Text}");
            string server_response = ServerCommunicator.SendCommandToServer(outgoing_payload);
            JsonDocument jdoc = JsonDocument.Parse(server_response);
            JsonElement root = jdoc.RootElement;
            if (root.TryGetProperty("STATUS", out JsonElement status))
            {
                switch (status.GetString())
                {
                    case "STATUS_OK":
                        Console.WriteLine("Login OK");
                        UserAuthenticated = true;
                        Username = lfUsernameTextBox.Text;
                        this.Close();
                        break;
                    case "STATUS_BAD_SALT":
                        Console.WriteLine("Password salt was incorrect, attempting to regenerate...");
                        string salt64 = root.GetProperty("SALT").GetString() ?? "ERR_BAD_SALT";
                        byte[] salt_reminder = Convert.FromBase64String(salt64);
                        for (int i = 0; i < salt_reminder.Length; ++i)
                        { UserConfig.Salt[i] = salt_reminder[i]; }
                        rehashing = true;
                        LoginButton_Click(sender, e);
                        break;
                    case "STATUS_PASSWORD_INCORRECT":
                        Console.WriteLine("Server indicated the input password was incorrect.");
                        MessageBox.Show("Invalid password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lfLoginButton.Enabled = true;
                        break;
                    default:
                        string msg = $"Unable to communicate with the server.";
                        var result = MessageBox.Show(msg, "Network error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (result == DialogResult.Retry) { LoginButton_Click(sender, e); } else { this.Close(); }
                        break;
                }
            }
        }

        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        { // Disallows non alphanumeric and underscore characters in username field. Spaces are not allowed. 
            var tb = (TextBox)sender;
            var cursorPos = tb.SelectionStart;
            string modified_text = Regex.Replace(tb.Text, "[^0-9a-zA-Z_]", "");
            if (modified_text != tb.Text) { SystemSounds.Asterisk.Play(); tb.Text = modified_text; }
            tb.SelectionStart = cursorPos;

            lfLoginButton.Enabled = tb.Text.Length >= 3; // Username must be between 3 and 20 characters. 
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        { lfLoginButton.Enabled = lfPasswordTextBox.Text.Length >= 8; }

        private ConfigData LoadUserConfig()
        {
            string path = Application.StartupPath + "/Users/" + lfUsernameTextBox.Text + ".config";
            if (!File.Exists(path))
            {
                return new(new List<FileData>(), new List<FileData>(), new byte[16]);
                // rebuild files list
            }

            using var reader = new BinaryReader(File.OpenRead(path));
            string encoded = reader.ReadString();
            byte[] bytes = Convert.FromBase64String(encoded);
            string json = Encoding.UTF8.GetString(bytes);

            return JsonSerializer.Deserialize<ConfigData>(json);
        }

    }
}
