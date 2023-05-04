namespace ZiplineClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        ///  
        /// privateVariable
        /// PublicVariable
        /// soft_const_variable
        /// HARD_CONST_VARIABLE
        /// 
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // It throws an exception if the MainForm is closed in its own constructor (because you closed the loginform)
            // Fix this eventually...
            try { Application.Run(new MainForm()); } catch { }
        }
    }
}