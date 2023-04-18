namespace ZiplineClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            LoginForm lf = new();
            Application.Run(lf);
            if (lf.UserAuthenticated) { Application.Run(new MainForm(lf.Username)); }
        }
    }
}