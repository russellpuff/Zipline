namespace ZiplineClient
{
    public partial class LoginForm : Form
    {
        public bool UserAuthenticated { get; private set; }
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Replace this with actual login code lol
            UserAuthenticated = true;
            this.Close();
        }
    }
}
