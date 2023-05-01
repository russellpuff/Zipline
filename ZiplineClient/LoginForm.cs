using System.Media;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace ZiplineClient
{
    public partial class LoginForm : Form
    {
        public bool UserAuthenticated { get; private set; }
        public string Username { get; private set; }
        public string CurrentIP { get; private set; }
        public LoginForm()
        {
            InitializeComponent();
            UserAuthenticated = false;
            Username = string.Empty;
            CurrentIP = string.Empty;
        }

        private async void LoginButton_ClickAsync(object sender, EventArgs e)
        {
            lfLoginButton.Enabled = false;
            string password = "null"; // Replace with password hashing.
            CurrentIP = await GetCurrentIP();
            if (CurrentIP == "STATUS_RETRY") { return; }
            var outgoing_payload = new
            {
                Command = "login_user",
                Username = lfUsernameTextBox.Text,
                Password = password,
                LatestIP = CurrentIP
            };
            string server_response = ServerCommunicator.SendCommandToServer(outgoing_payload);
            if (server_response.Contains("OK"))
            {
                UserAuthenticated = true;
                Username = lfUsernameTextBox.Text;
                this.Close();
            }
            else if (server_response is not "STATUS_FAILURE")
            { MessageBox.Show("Invalid password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                string msg = $"Unable to communicate with the server.";
                var result = MessageBox.Show(msg, "Network error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Retry) { LoginButton_ClickAsync(sender, e); } else { this.Close(); }
            }
        }

        private async Task<string> GetCurrentIP()
        {
            string current_ip = string.Empty;
            try
            { // Request public ip. 
                using HttpClient httpClient = new();
                HttpResponseMessage response = await httpClient.GetAsync("https://api64.ipify.org");
                if (response.IsSuccessStatusCode) 
                { current_ip = await response.Content.ReadAsStringAsync(); }
            }
            catch
            {
                string msg = "Couldn't retrieve current IP. Make sure you are connected to the internet.";
                var result = MessageBox.Show(msg, "IP Retrieve Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Cancel) { this.Close(); }
                else { current_ip = "STATUS_RETRY"; } // bootleg solution
            }
            return current_ip;
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
    }
}
