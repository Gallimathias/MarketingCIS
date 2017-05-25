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
    public class TcpConnection
    {
        public const int MAX_EMPTYCOUNT = 10;

        public NetworkStream Stream
        {
            get
            {
                if (!tcpClient.Connected)
                    return null;
                return tcpClient?.GetStream();
            }
        }
        public bool Connected => tcpClient.Connected;
        public bool IsSecure { get; private set; }
        public byte[] Key
        {
            get => key;
            set
            {
                key = value;
                IsSecure = true;
            }
        }
        public int Id { get; set; }
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

        public delegate void MessageEventHandler(TcpConnection tcpConnection, NetworkMessage networkMessage);
        public delegate void ConnectionEventHandler(TcpConnection tcpConnection);
        public event MessageEventHandler OnMessageRecived;
        public event ConnectionEventHandler OnDisconnect;
        public event ConnectionEventHandler OnConnect;

        public TcpConnection(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            if (tcpClient.Connected)
                OnConnect?.Invoke(this);

            tokenSource = new CancellationTokenSource();
        }
        public TcpConnection(string host, int port) : this(new TcpClient(host, port)) { }

        public void Disconnect()
        {
            Send(new NetworkMessage("disconnect", new byte[0]));
            tokenSource?.Cancel();
            Stream?.Close();
            tcpClient?.Close();
            OnDisconnect?.Invoke(this);
        }

        public void Connect(string host, int port)
        {
            tcpClient?.Connect(host, port);

            if (tcpClient.Connected)
                OnConnect?.Invoke(this);
        }

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

        public NetworkMessage ReciveMessage() => IsSecure ? ReadSecure() : Read();

        public void Send(NetworkMessage message)
        {
            if (IsSecure)
                WriteSecure(message);
            else
                Write(message);
        }

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