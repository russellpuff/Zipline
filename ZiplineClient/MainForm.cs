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
        readonly byte[] delim_bytes = Encoding.ASCII.GetBytes("<END>");
        readonly IPEndPoint server_endpoint = new(IPAddress.Parse("127.0.0.1"), 9999);
        public MainForm()
        {
            InitializeComponent();
            GetUsersAndFiles();
        }

        private void GetUsersAndFiles()
        { // Method queries server for a list of the online users and shared files. 
            string command = "get_users_files";
            byte[] cmd_bytes = Encoding.UTF8.GetBytes(command);
            int pkg_length = 12 + cmd_bytes.Length + delim_bytes.Length;
            byte[] length_bytes = BitConverter.GetBytes(pkg_length);
            byte[] package = new byte[pkg_length];

            length_bytes.CopyTo(package, 0);
            header_bytes.CopyTo(package, 4);
            cmd_bytes.CopyTo(package, 12);
            Array.Copy(delim_bytes, 0, package, package.Length - delim_bytes.Length, delim_bytes.Length); // Copy delimiter to the end.

            TcpClient tclient = new();
            tclient.Connect(server_endpoint);
            try
            {
                NetworkStream stream = tclient.GetStream();
                stream.Write(package, 0, package.Length);





            } catch (Exception ex)
            {
                string msg = $"Unable to communicate with the server.\nInfo: {ex.Message}";
                var result = MessageBox.Show(msg, "Network error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Retry) { GetUsersAndFiles(); } else { this.Close(); }
            } finally { tclient.Close(); }
        }
    }

}
