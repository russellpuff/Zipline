using System.Diagnostics;
using System.Net.Sockets;

namespace ZiplineClient
{
    public partial class DownloadForm : Form
    {
        NetworkStream networkStream;
        int fileSize;
        string filename;
        public DownloadForm(NetworkStream _network, int _fileSize, string _filename)
        {
            InitializeComponent();
            networkStream = _network;
            fileSize = _fileSize;
            filename = Application.StartupPath + "/Downloads/" + _filename;
            if (File.Exists(filename)) { filename = Application.StartupPath + "/Downloads/Copy_of_" + _filename; }
            this.DialogResult = DialogResult.None;
            Task.Run(() => DownloadFile());
        }

        public void DownloadFile()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int bytesRead, totalBytesRead = 0;
            byte[] file_bytes = new byte[fileSize];
            do
            {
                bytesRead = networkStream.Read(file_bytes, totalBytesRead, file_bytes.Length - totalBytesRead);
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
            using FileStream fs = new(filename, FileMode.Create, FileAccess.Write);
            fs.Write(file_bytes, 0, fileSize);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object? sender, EventArgs? e)
        {
            networkStream.Dispose();
            networkStream.Close();
            this.DialogResult = DialogResult.Cancel;
            this.Close(); 
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None) { CancelButton_Click(null, null); }
        }
    }
}
