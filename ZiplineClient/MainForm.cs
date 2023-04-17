using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZiplineClient
{
    public partial class MainForm : Form
    {
        readonly byte[] header_bytes = { 0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D };
        readonly IPEndPoint server_endpoint = new(IPAddress.Parse("137.184.241.2"), 52525);
        public MainForm()
        {
            InitializeComponent();
            GetUsersAndFiles();
        }

        private void GetUsersAndFiles()
        { // Method queries server for a list of the online users and shared files. 
            string command = "get_users_files";
            byte[] cmd_bytes = Encoding.UTF8.GetBytes(command);
            int pkg_length = 12 + cmd_bytes.Length;
            byte[] length_bytes = BitConverter.GetBytes(pkg_length);
            byte[] outgoing_package = new byte[pkg_length];

            length_bytes.CopyTo(outgoing_package, 0);
            header_bytes.CopyTo(outgoing_package, 4);
            cmd_bytes.CopyTo(outgoing_package, 12);

            TcpClient tclient = new();
            tclient.Connect(server_endpoint);
            try
            {
                NetworkStream stream = tclient.GetStream();
                stream.Write(outgoing_package, 0, outgoing_package.Length);

                byte[] size_data = new byte[4];
                int bytesRead = 0, totalBytesRead = 0;
                do
                {
                    bytesRead = stream.Read(size_data, totalBytesRead, size_data.Length - totalBytesRead);
                    totalBytesRead += bytesRead;
                } while (totalBytesRead < 4);
                int package_length = BitConverter.ToInt32(size_data, 0);

                byte[] incoming_package = new byte[package_length];
                totalBytesRead = 0;
                do
                {
                    bytesRead = stream.Read(incoming_package, totalBytesRead, package_length - totalBytesRead);
                    totalBytesRead += bytesRead;
                } while (totalBytesRead < package_length);

                string json = Encoding.UTF8.GetString(incoming_package);
                MessageBox.Show(json);

            } catch (Exception ex)
            {
                string msg = $"Unable to communicate with the server.\nInfo: {ex.Message}";
                var result = MessageBox.Show(msg, "Network error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Retry) { GetUsersAndFiles(); } else { this.Close(); }
            } finally { tclient.Close(); }
        }
    }

}
