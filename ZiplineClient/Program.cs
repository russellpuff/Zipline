using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text;

namespace ZiplineClient
{
    internal static class Program
    {
        private static readonly byte[] header_bytes = { 0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D };
        private static readonly IPEndPoint server_endpoint = new(IPAddress.Parse("137.184.241.2"), 52525);
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // It throws an exception if the MainForm is closed in its own constructor (because you closed the loginform)
            // Fix this eventually...
            try { Application.Run(new MainForm()); } catch { }
        }

        /// <summary>
        /// Method serves as a generic communication point for talking to the server. 
        /// Since the outgoing communication and incoming response use the same format,
        /// this will process all the work on its own and then return a string containing
        /// the server's response, the calling method must decipher it. 
        /// </summary>
        public static async Task<string> SendCommandToServerAsync(dynamic payload)
        {
            string json = JsonSerializer.Serialize(payload);
            CancellationTokenSource cts = new CancellationTokenSource(5000);
            Task<string> serverRequest = CommunicateWithServerAsync(json);
            Task completedTask = await Task.WhenAny(serverRequest, Task.Delay(-1, cts.Token));
            return completedTask == serverRequest ? await serverRequest : "STATUS_TIMEOUT";
        }

        static async Task<string> CommunicateWithServerAsync(string json)
        {
            string retVal = string.Empty;
            
            byte[] payload_bytes = Encoding.UTF8.GetBytes(json);
            int pkg_length = 12 + payload_bytes.Length; // Length 4 bytes + header 8 bytes + payload
            byte[] length_bytes = BitConverter.GetBytes(pkg_length);
            if (BitConverter.IsLittleEndian) { Array.Reverse(length_bytes); }
            byte[] outgoing_package = new byte[pkg_length];

            length_bytes.CopyTo(outgoing_package, 0);
            header_bytes.CopyTo(outgoing_package, 4);
            payload_bytes.CopyTo(outgoing_package, 12);

            TcpClient tclient = new();
            try
            {
                tclient.Connect(server_endpoint);
                NetworkStream stream = tclient.GetStream();
                stream.Write(outgoing_package, 0, outgoing_package.Length);

                byte[] size_data = new byte[4];
                int bytesRead = 0, totalBytesRead = 0;
                do
                {
                    bytesRead = stream.Read(size_data, totalBytesRead, size_data.Length - totalBytesRead);
                    totalBytesRead += bytesRead;
                } while (totalBytesRead < 4);
                if (BitConverter.IsLittleEndian) { Array.Reverse(size_data); }
                int package_length = BitConverter.ToInt32(size_data, 0) - 4;

                byte[] incoming_package = new byte[package_length];
                totalBytesRead = 0;
                do
                {
                    bytesRead = stream.Read(incoming_package, totalBytesRead, package_length - totalBytesRead);
                    totalBytesRead += bytesRead;
                } while (totalBytesRead < package_length);

                retVal = Encoding.UTF8.GetString(incoming_package);
            }
            catch
            {
                retVal = "STATUS_FAILURE";
            }
            finally { tclient.Close(); }

            return retVal;
        }

        public static void SendCommandToUser(dynamic payload, string ip, string port)
        {
            string json = JsonSerializer.Serialize(payload);
            byte[] payload_bytes = Encoding.UTF8.GetBytes(json);
            int pkg_length = 12 + payload_bytes.Length; // Length 4 bytes + header 8 bytes + payload
            byte[] length_bytes = BitConverter.GetBytes(pkg_length);
            if (BitConverter.IsLittleEndian) { Array.Reverse(length_bytes); }
            byte[] outgoing_package = new byte[pkg_length];
            byte[] header_bytes = { 0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D };

            length_bytes.CopyTo(outgoing_package, 0);
            header_bytes.CopyTo(outgoing_package, 4);
            payload_bytes.CopyTo(outgoing_package, 12);

            TcpClient tclient = new();
            try
            {
                tclient.Connect(new(IPAddress.Parse(ip), int.Parse(port)));
                NetworkStream stream = tclient.GetStream();
                stream.Write(outgoing_package, 0, outgoing_package.Length);
            }
            catch (Exception ex)
            {
                string mesg = $"Unable to communicate with the other user.\nInfo{ex.Message}";
                MessageBox.Show(mesg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { tclient.Close(); }
        }
    }
}