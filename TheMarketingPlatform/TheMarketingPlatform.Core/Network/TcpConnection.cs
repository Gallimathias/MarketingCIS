using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.Network;
using TheMarketingPlatform.Core.Secure;

namespace TheMarketingPlatform.Core.Network
{
    /// <summary>
    /// Represents a connection via tcp
    /// </summary>
    public class TcpConnection
    {
        /// <summary>
        /// Max no. of allowed failed messages
        /// </summary>
        public const int MAX_EMPTYCOUNT = 10;

        /// <summary>
        /// The NetworkStream from the base tcpClient
        /// </summary>
        public NetworkStream Stream
        {
            get
            {
                if (!tcpClient.Connected)
                    return null;
                return tcpClient?.GetStream();
            }
        }
        /// <summary>
        /// Returns true if the base tcpClient is connected
        /// </summary>
        public bool Connected => tcpClient.Connected;
        /// <summary>
        /// Returns true if the connection has a key and use a secure tunnel
        /// </summary>
        public bool IsSecure { get; private set; }
        /// <summary>
        /// Key from diffiehellman key exchange. If set then the connection is secure
        /// </summary>
        public byte[] Key
        {
            get => key;
            set
            {
                key = value;
                IsSecure = true;
            }
        }
        /// <summary>
        /// The id of the connection in the server. Only serverside
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Count of failed messages
        /// </summary>
        public int EmptyCount
        {
            get => emptyCount;
            set
            {
                emptyCount = value;
                if (emptyCount >= MAX_EMPTYCOUNT)
                    Disconnect();
            }
        }

        private TcpClient tcpClient;
        private byte[] key;
        private CancellationTokenSource tokenSource;
        private int emptyCount;

        /// <summary>
        /// Handles message events
        /// </summary>
        /// <param name="tcpConnection">The sender</param>
        /// <param name="networkMessage">The throwing message</param>
        public delegate void MessageEventHandler(TcpConnection tcpConnection, NetworkMessage networkMessage);
        /// <summary>
        /// Handles connection events
        /// </summary>
        /// <param name="tcpConnection">the sending connection</param>
        public delegate void ConnectionEventHandler(TcpConnection tcpConnection);
        /// <summary>
        /// Thrown if a message is recived
        /// </summary>
        public event MessageEventHandler OnMessageRecived;
        /// <summary>
        /// Thrown on disconnect
        /// </summary>
        public event ConnectionEventHandler OnDisconnect;
        /// <summary>
        /// Thrown on connect
        /// </summary>
        public event ConnectionEventHandler OnConnect;

        /// <summary>
        /// Represents a connection via tcp. From exist connection
        /// </summary>
        /// <param name="tcpClient">a connected tcp client</param>
        public TcpConnection(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            if (tcpClient.Connected)
                OnConnect?.Invoke(this);

            tokenSource = new CancellationTokenSource();
        }
        /// <summary>
        /// Represents a connection via tcp. Implements a new connection
        /// </summary>
        /// <param name="host">the target host</param>
        /// <param name="port">the target port</param>
        public TcpConnection(string host, int port) : this(new TcpClient(host, port)) { }

        /// <summary>
        /// Close this connection
        /// </summary>
        public void Disconnect()
        {
            Send(new NetworkMessage("disconnect", new byte[0]));
            tokenSource?.Cancel();
            Stream?.Close();
            tcpClient?.Close();
            OnDisconnect?.Invoke(this);
        }

        /// <summary>
        /// Start or restart the connection
        /// </summary>
        /// <param name="host">The target host</param>
        /// <param name="port">The target port</param>
        public void Connect(string host, int port)
        {
            tcpClient?.Connect(host, port);

            if (tcpClient.Connected)
                OnConnect?.Invoke(this);
        }

        /// <summary>
        /// Beginns async listening
        /// </summary>
        public void BeginRecive()
        {
            Task.Run(() =>
            {
                var message = ReciveMessage();

                if (!message.IsEmpty)
                    OnMessageRecived?.Invoke(this, message);

                if (Connected)
                    BeginRecive();
                else
                    OnDisconnect?.Invoke(this);
            }, tokenSource.Token);
        }

        /// <summary>
        /// Wait on recive
        /// </summary>
        /// <returns>The recived message</returns>
        public NetworkMessage ReciveMessage() => IsSecure ? ReadSecure() : Read();

        /// <summary>
        /// Sends a network message
        /// </summary>
        /// <param name="message">the message to send</param>
        public void Send(NetworkMessage message)
        {
            if (IsSecure)
                WriteSecure(message);
            else
                Write(message);
        }

        /// <summary>
        /// Sends a network message async
        /// </summary>
        /// <param name="message">the message to send</param>
        public void SendAsync(NetworkMessage message) => Task.Run(() => Send(message), tokenSource.Token);


        private NetworkMessage ReadSecure()
        {
            if (!Connected)
                return NetworkMessage.EmptyMessage;

            var data = new byte[0];
            var vector = new byte[0];

            try
            {
                using (var stream = new MemoryStream(ReadStream()))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        vector = reader.ReadBytes(reader.ReadInt32());
                        data = reader.ReadBytes(reader.ReadInt32());
                    }
                }

                return NetworkMessage.Deserialize(Encryption.Decrypt(data, key, vector));
            }
            catch (Exception)
            {
                EmptyCount++;
                return NetworkMessage.EmptyMessage;
            }
        }

        private NetworkMessage Read() => NetworkMessage.Deserialize(ReadStream());

        private void WriteSecure(NetworkMessage message)
        {
            if (!Connected)
                return;

            try
            {
                var vector = Encryption.GetVector();
                var data = Encryption.Encrypt(NetworkMessage.Serialize(message), key, vector);

                using (var stream = new MemoryStream())
                {
                    using (var writer = new BinaryWriter(stream))
                    {
                        writer.Write(vector.Length);
                        writer.Write(vector);
                        writer.Write(data.Length);
                        writer.Write(data);
                    }
                    WriteStream(stream.ToArray());
                }
            }
            catch (Exception)
            {
                EmptyCount++;
                return;
            }
        }

        private void Write(NetworkMessage message) => WriteStream(NetworkMessage.Serialize(message));

        private byte[] ReadStream()
        {
            var data = new byte[0];

            try
            {
                using (var reader = new BinaryReader(Stream, Encoding.UTF8, true))
                {
                    data = reader.ReadBytes(reader.ReadInt32());
                }
            }
            catch (Exception)
            {
                EmptyCount++;
                data = null;
            }

            return data;
        }

        private void WriteStream(byte[] data)
        {
            try
            {
                using (var writer = new BinaryWriter(Stream, Encoding.UTF8, true))
                {
                    writer.Write(data.Length);
                    writer.Write(data);
                    writer.Flush();
                }
            }
            catch (Exception)
            {
                EmptyCount++;
                return;
            }
        }

        /// <summary>
        /// Converts a byte[] list to a singel dimension array
        /// </summary>
        /// <param name="dataList">A list of byte arrays</param>
        /// <returns>a singel dimension array</returns>
        public static byte[] GetDataFromList(List<byte[]> dataList)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(dataList.Count);
                    foreach (var byteArray in dataList)
                    {
                        writer.Write(byteArray.Length);
                        writer.Write(byteArray);
                    }
                }
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Converts a single dimension byte array to a byte array list
        /// </summary>
        /// <param name="data">a single dimension byte array</param>
        /// <returns>returns a list of byte arrays</returns>
        public static List<byte[]> GetListFromData(byte[] data)
        {
            var list = new List<byte[]>();
            using (var stream = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var count = reader.ReadUInt32();

                    for (int i = 0; i < count; i++)
                        list.Add(
                            reader.ReadBytes(reader.ReadInt32()));
                }
            }

            return list;
        }
    }
}