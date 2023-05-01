using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text;

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
            // It throws an exception if the MainForm is closed in its own constructor (because you closed the loginform)
            // Fix this eventually...
            try { Application.Run(new MainForm()); } catch { }
        }
    }

    public sealed class ServerConnection
    {
        private static readonly ServerConnection instance = new();

        private readonly Socket client_socket;
        public readonly object streamLock = new();
        public ServerConnection()
        {
            client_socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client_socket.Connect(new IPEndPoint(IPAddress.Parse("137.184.241.2"), 52525));
        }

        public static ServerConnection Instance
        {
            get { return instance; }
        }

        public Socket Socket
        {
            get { return client_socket; }
            set { }
        }
    }

    internal static class ServerCommunicator
    {
        /// <summary>
        /// Method serves as a generic communication point for talking to the server. 
        /// Since the outgoing communication and incoming response use the same format,
        /// this will process all the work on its own and then return a string containing
        /// the server's response, the calling method must decipher it. 
        /// </summary>
        private static readonly byte[] header_bytes = { 0x63, 0xC3, 0x1E, 0xE9, 0xF0, 0x58, 0x60, 0x1D };

        public static string SendCommandToServer(dynamic payload)
        {
            string json = JsonSerializer.Serialize(payload);
            string retVal = string.Empty;

            byte[] payload_bytes = Encoding.UTF8.GetBytes(json);
            int pkg_length = 12 + payload_bytes.Length; // Length 4 bytes + header 8 bytes + payload
            byte[] length_bytes = BitConverter.GetBytes(pkg_length);
            if (BitConverter.IsLittleEndian) { Array.Reverse(length_bytes); }
            byte[] outgoing_package = new byte[pkg_length];

            length_bytes.CopyTo(outgoing_package, 0);
            header_bytes.CopyTo(outgoing_package, 4);
            payload_bytes.CopyTo(outgoing_package, 12);

            lock (ServerConnection.Instance.streamLock)
            {
                try
                {
                    ServerConnection.Instance.Socket.Send(outgoing_package);

                    byte[] size_data = new byte[4];
                    int bytesRead = 0, totalBytesRead = 0;
                    do
                    {
                        bytesRead = ServerConnection.Instance.Socket.Receive(
                            size_data, totalBytesRead, size_data.Length - totalBytesRead, SocketFlags.None);
                        totalBytesRead += bytesRead;
                    } while (totalBytesRead < 4);
                    if (BitConverter.IsLittleEndian) { Array.Reverse(size_data); }
                    int package_length = BitConverter.ToInt32(size_data, 0) - 4;

                    const int maxDataSize = 52429000; // 50mb in bytes + 200bytes flex room.
                    if (package_length <= maxDataSize)
                    {
                        byte[] incoming_package = new byte[package_length];
                        totalBytesRead = 0;
                        do
                        {
                            bytesRead = ServerConnection.Instance.Socket.Receive(
                                incoming_package, totalBytesRead, package_length - totalBytesRead, SocketFlags.None);
                            totalBytesRead += bytesRead;
                        } while (totalBytesRead < package_length);
                        retVal = Encoding.UTF8.GetString(incoming_package);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //ServerConnection.Instance.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //ServerConnection.Instance.Socket.Connect(new IPEndPoint(IPAddress.Parse("137.184.241.2"), 52525));
                    //return SendCommandToServer(payload);
                }
            }
            return retVal;
        }
    }
}