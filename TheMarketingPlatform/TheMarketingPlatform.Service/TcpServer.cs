using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CommandManagementSystem;
using TheMarketingPlatform.Core.Network;

namespace TheMarketingPlatform.Service
{
    internal class TcpServer : TcpListener
    {
        public ConcurrentDictionary<int, TcpConnection> Connections { get; private set; }
        public SettingsHandler SettingsHandler { get; internal set; }
        public ServiceCommandManager CommandManager { get; internal set; }

        public delegate void ServerEventHandler(TcpConnection client);
        public event ServerEventHandler ClientConnect;
        public event ServerEventHandler ClientReady;

        public TcpServer(IPAddress localIPAddress, int port) : base(localIPAddress, port)
        {
            Connections = new ConcurrentDictionary<int, TcpConnection>();
        }

        public new void Start()
        {
            base.Start();
            BeginAcceptTcpClient(HandShake, null);
        }

        public new void Stop()
        {
            foreach (var connection in Connections)
                connection.Value.Disconnect();

            base.Stop();
        }

        private void HandShake(IAsyncResult ar)
        {
            BeginAcceptTcpClient(HandShake, null);
            var client = new TcpConnection(EndAcceptTcpClient(ar));
            ClientConnect?.Invoke(client);
            if (client.ReciveMessage().IsEmpty)
                return;

            client.Send(NetworkMessage.DefaultOk);

            DiffieHellman(client);

            if (client.ReciveMessage().IsEmpty)
                return;

            var random = new Random();
            var id = random.Next();

            while (Connections.ContainsKey(id))
                id = random.Next();

            if (!Connections.TryAdd(id, client))
            {
                client.Send(NetworkMessage.DefaultError);
                client.Disconnect();
            }

            client.Id = id;

            client.Send(NetworkMessage.DefaultOk);

            //client.OnMessageRecived += Client_OnMessageRecived;
            client.OnDisconnect += Client_OnDisconnect;
            //client.BeginRecive();
            ClientReady?.Invoke(client);
            Client_OnMessageRecived(client, client.ReciveMessage());
        }

        private void Client_OnDisconnect(TcpConnection tcpConnection) =>
            Connections.TryRemove(tcpConnection.Id, out tcpConnection);

        private void Client_OnMessageRecived(TcpConnection tcpConnection, NetworkMessage networkMessage)
        {
            CommandManager.DispatchAsync(
                  networkMessage.Tag, new KeyValuePair<TcpConnection, byte[]>(tcpConnection, networkMessage.Payload));

            if (tcpConnection.Connected)
                Client_OnMessageRecived(tcpConnection, tcpConnection.ReciveMessage());
        }

        private void DiffieHellman(TcpConnection client)
        {
            using (var ecd = new ECDiffieHellmanCng()
            {
                KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash,
                HashAlgorithm = CngAlgorithm.Sha256
            })
            {
                var publicKey = client.ReciveMessage().Payload;

                client.Send(new NetworkMessage(null, ecd.PublicKey.ToByteArray()));
                client.Key = ecd.DeriveKeyMaterial(CngKey.Import(publicKey, CngKeyBlobFormat.EccPublicBlob));
            }
        }
    }
}
