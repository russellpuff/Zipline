using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Text;
using System.Security.Cryptography;

namespace ZiplineClient
{
    public sealed class ServerConnection
    {
        private static ServerConnection instance = new();

        private readonly Socket client_socket;
        public readonly object streamLock = new();
        public ServerConnection()
        {
            client_socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client_socket.Connect(new IPEndPoint(IPAddress.Parse("137.184.241.2"), 52525));
        }

        public static ServerConnection Instance
        {
            get
            {  
                try
                {
                    return instance;
                } catch
                {
                    instance = new();
                    return instance;
                }
            }
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

        // Sends a dynamic payload to the server and returns the server's response.
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
                    if (json.Contains("logout_user") || json.Contains("download_file")) { return string.Empty; } // Shitty patchfix LOL

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

                    const int MAX_DATA_SIZE = 52429000; // 50mb in bytes + 200bytes flex room.
                    if (package_length <= MAX_DATA_SIZE)
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
                }
            }
            if (retVal.Length <= 1 ) { retVal = "STATUS_FAILURE"; } // A patchfix to a communication error. 
            return retVal;
        }

        // Hashing algorithm for logging in and changing user password. 
       public static string HashPassword(string password, object config, string propertyName, bool newPass = false)
        {
#nullable disable
            var prop = config.GetType().GetProperty(propertyName); // Fuckery to bypass not being able to ref properties. 
            var user_salt = (byte[])prop.GetValue(config);

            bool saltExists = false;
            foreach (byte b in user_salt) { if (b != 0) { saltExists = true; } }
            byte[] salt = new byte[16];
            if (!saltExists || newPass)
            {   // Generate new salt if one doesn't exist OR a new password is being made. 
                // Note that if on login, if the salt isn't correct this will run again with (ideally) the correct salt. 
                RandomNumberGenerator.Create().GetBytes(salt);
                for (int i = 0; i < salt.Length; ++i)
                { user_salt[i] = salt[i]; }
            }

            salt = user_salt;
            var pbk = new Rfc2898DeriveBytes(password, salt, 49106);
            byte[] pass = pbk.GetBytes(20);

            byte[] hash_bytes = new byte[36];
            Array.Copy(salt, 0, hash_bytes, 0, 16);
            Array.Copy(pass, 0, hash_bytes, 16, 20);

            return Convert.ToBase64String(hash_bytes);
#nullable enable
        }
    }
}