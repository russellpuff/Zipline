using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ZiplineClient
{
    public partial class FileTransferForm : Form
    {
        private bool cancelling = false;
        private readonly bool downloading;
        readonly int file_size;
        readonly string filename;
        readonly string target_user = default!;
        // Constructor for when the FileTransferForm needs to download a pending file in the Socket stream.
        public FileTransferForm(int _fileSize, string _filename)
        {
            InitializeComponent();
            Console.WriteLine("Initializing file transfer form in download mode.");
            this.Text = "Download";
            dfDownloadingLabel.Text = "Downloading. . .";
            downloading = true;
            file_size = _fileSize;
            filename = Application.StartupPath + "/Downloads/" + _filename;
            if (File.Exists(filename))
            {
                string fn = Path.GetFileNameWithoutExtension(_filename);
                string ext = Path.GetExtension(_filename);
                int i = 1;
                while (true) // Append a number to the end of the duplicate filename, acknowedges dupes of dupes.
                {
                    filename = Application.StartupPath + "/Downloads/" + fn + " (" + i + ")" + ext;
                    if (!File.Exists(filename)) { break; }
                    ++i;
                }
            }

            this.DialogResult = DialogResult.None;
        }

        // Constructor for when the FileTransfer form needs to upload a file for another user. 
        public FileTransferForm(string _filename, string _destinationUser)
        {
            InitializeComponent();
            Console.WriteLine("Initializing file transfer form in upload mode.");
            this.Text = "Upload";
            dfDownloadingLabel.Text = "Uploading. . .";
            downloading = false;
            target_user = _destinationUser;
            filename = Application.StartupPath + "/Shared Files/" + _filename;

            this.DialogResult = DialogResult.None;
        }

        private void DownloadFile()
        {
            Console.WriteLine("Spinning up downloader...");
            // No lock because this should be locked by MainForm.ServerListener()
            int bytesRead, totalBytesRead = 0;
            byte[] file_bytes = new byte[file_size];
            do
            {
                if (cancelling) { return; }
                bytesRead = ServerConnection.Instance.Socket.Receive(
                    file_bytes, totalBytesRead, file_size - totalBytesRead, SocketFlags.None);
                totalBytesRead += bytesRead;
                dfDownloadProgressBar.Value = (int)(totalBytesRead * 100 / file_size);

                Console.WriteLine($"Downloaded {totalBytesRead} / {file_size} bytes");
            } while (totalBytesRead < file_size);
            dfDownloadProgressBar.Value = 100;

            string base64string = Encoding.UTF8.GetString(file_bytes);
            byte[] output_file = Convert.FromBase64String(base64string);

            using FileStream fs = new(filename, FileMode.Create, FileAccess.Write);
            fs.Write(output_file, 0, output_file.Length);
            this.DialogResult = DialogResult.OK;
        }

        private void UploadFile()
        {
            Console.WriteLine("Spinning up uploader...");
            try
            {
                // No lock because this should be locked by MainForm.ServerListener()
                FileStream fs = new(filename, FileMode.Open, FileAccess.Read);
                BinaryReader br = new(fs);

                byte[] file_bytes = br.ReadBytes((int)fs.Length);
                string base64File = Convert.ToBase64String(file_bytes);

                var outgoing_payload = new
                {
                    Command = "send_file",
                    Response = "STATUS_OK",
                    TargetUser = target_user,
                    File = base64File
                };

                string json = JsonSerializer.Serialize(outgoing_payload);
                byte[] header_bytes = { 0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D };

                byte[] payload_bytes = Encoding.UTF8.GetBytes(json);
                int pkg_length = 12 + payload_bytes.Length; // Length 4 bytes + header 8 bytes + payload
                byte[] length_bytes = BitConverter.GetBytes(pkg_length);
                if (BitConverter.IsLittleEndian) { Array.Reverse(length_bytes); }
                byte[] outgoing_package = new byte[pkg_length];

                length_bytes.CopyTo(outgoing_package, 0);
                header_bytes.CopyTo(outgoing_package, 4);
                payload_bytes.CopyTo(outgoing_package, 12);

                try
                {
                    int totalBytesSent = 0;
                    const int CHUNK_SIZE = 100;
                    for (int i = 0; i < pkg_length; i += CHUNK_SIZE)
                    {
                        int bytesRemain = pkg_length - i;
                        int bytesToSend = Math.Min(bytesRemain, CHUNK_SIZE);
                        ServerConnection.Instance.Socket.Send(outgoing_package, i, bytesToSend, SocketFlags.None);
                        totalBytesSent += bytesToSend;
                        // Update progress bar.
                        int progress = (int)((totalBytesSent * 100) / pkg_length);
                        dfDownloadProgressBar.Value = progress;
                        Console.WriteLine($"Uploaded {totalBytesSent} / {pkg_length} bytes.");
                    }

                    dfDownloadProgressBar.Value = 100;
                    this.DialogResult = DialogResult.OK;
                    DiscardStream();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Couldn't send response to server: {ex.Message}");
                }
                Console.WriteLine("Finished sending a file to the server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't send file: {ex.Message}");
                var outgoing_payload = new
                {
                    Command = "send_file",
                    Response = "STATUS_FAILURE",
                    TargetUser = target_user
                };
                ServerCommunicator.SendCommandToServer(outgoing_payload);
            }
        }

        private void CancelButton_Click(object? sender, EventArgs? e)
        {
            cancelling = true;
            DiscardStream();
            this.DialogResult = DialogResult.Cancel;
            this.Close(); 
        }

        private static void DiscardStream()
        {
            byte[] discard_buffer = new byte[1024];
            while (ServerConnection.Instance.Socket.Available > 0)
            { ServerConnection.Instance.Socket.Receive(discard_buffer, SocketFlags.None); }
        }

        private void DownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None) { CancelButton_Click(null, null); }
        }

        private void DownloadForm_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            if (downloading) { DownloadFile(); } 
            else { UploadFile(); } // Uploading
            this.Close();
        }
    }
}
