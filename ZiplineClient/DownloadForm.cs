using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace ZiplineClient
{
    public partial class DownloadForm : Form
    {
        bool cancelling = false;
        readonly int fileSize;
        readonly string filename;
        public DownloadForm(int _fileSize, string _filename)
        {
            InitializeComponent();
            fileSize = _fileSize;
            filename = Application.StartupPath + "/Downloads/" + _filename;
            if (File.Exists(filename)) { filename = Application.StartupPath + "/Downloads/Copy_of_" + _filename; }
            this.DialogResult = DialogResult.None;
        }

        public void DownloadFile()
        {
            Console.WriteLine("Spinning up downloader...");
            Thread.Sleep(1000);
            // No lock because this should be locked by MainForm.ServerListener()
            Stopwatch stopwatch = Stopwatch.StartNew();
            int bytesRead, totalBytesRead = 0;
            byte[] file_bytes = new byte[fileSize];
            do
            {
                if (cancelling) { return; }
                bytesRead = ServerConnection.Instance.Socket.Receive(
                    file_bytes, totalBytesRead, fileSize - totalBytesRead, SocketFlags.None);
                totalBytesRead += bytesRead;
                if (stopwatch.ElapsedMilliseconds >= 1000)
                {
                    int dl_speed = (int)((totalBytesRead / 1024) / stopwatch.Elapsed.TotalSeconds);
                    dfDownloadingLabel.Text = "Downloading. . ." + dl_speed.ToString() + " kb/s";
                    stopwatch.Restart();
                    dfDownloadProgressBar.Value = (int)(totalBytesRead / fileSize);
                }
            } while (totalBytesRead < fileSize);
            dfDownloadProgressBar.Value = 100;

            string base64string = Encoding.UTF8.GetString(file_bytes);
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "base64string.txt");
            File.WriteAllText(filePath, base64string);
            byte[] output_file = Convert.FromBase64String(base64string);

            using FileStream fs = new(filename, FileMode.Create, FileAccess.Write);
            fs.Write(output_file, 0, output_file.Length);
            this.DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object? sender, EventArgs? e)
        {
            cancelling = true;
            lock (ServerConnection.Instance.streamLock)
            {
                byte[] discard_buffer = new byte[1024];
                while (ServerConnection.Instance.Socket.Available > 0)
                { ServerConnection.Instance.Socket.Receive(discard_buffer, SocketFlags.None); }
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close(); 
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None) { CancelButton_Click(null, null); }
        }

        private void DownloadForm_Shown(object sender, EventArgs e)
        {
            DownloadFile();
            this.Close();
        }
    }
}
