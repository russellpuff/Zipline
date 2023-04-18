using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace ZiplineClient
{
    public partial class LoginForm : Form
    {
        public bool UserAuthenticated { get; private set; }
        public string Username { get; private set; }
        public LoginForm()
        {
            InitializeComponent();
            UserAuthenticated = false;
            Username = string.Empty;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (lfUsernameTextBox.Text.Length == 0) { return; }
            // Replace this with actual login code lol
            UserAuthenticated = true;
            Username = lfUsernameTextBox.Text;
            this.Close();
        }

        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        { // Disallows non alphanumeric and underscore characters in username field. Spaces are not allowed. 
            var tb = (TextBox)sender;
            var cursorPos = tb.SelectionStart;
            string modified_text = Regex.Replace(tb.Text, "[^0-9a-zA-Z_]", "");
            if (modified_text != tb.Text) { SystemSounds.Asterisk.Play(); tb.Text = modified_text; }
            tb.SelectionStart = cursorPos;
        }
    }
}
