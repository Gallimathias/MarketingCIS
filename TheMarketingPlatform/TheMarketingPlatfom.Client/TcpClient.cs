using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.Network;

namespace TheMarketingPlatform.Client
{
    /// <summary>
    /// A Implementation of tcpConnection with diffieHellamn
    /// </summary>
    public class TcpClient : TcpConnection
    {
        public TcpClient(string host, int port) : base(host, port)
        {
        }

        /// <summary>
        /// Handshake with server. Contains a diffieHellmann key exchange
        /// </summary>
        public void Connect()
        {
            Send(NetworkMessage.DefaultOk);

            if (ReciveMessage().IsEmpty)
                return;

            using (var ecd = new ECDiffieHellmanCng() {
                KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash,
                HashAlgorithm = CngAlgorithm.Sha256
            })
            {
                Send(new NetworkMessage(null, ecd.PublicKey.ToByteArray()));
                var publicKey = ReciveMessage().Payload;

                Key = ecd.DeriveKeyMaterial(CngKey.Import(publicKey, CngKeyBlobFormat.EccPublicBlob));
            }

            Send(NetworkMessage.DefaultOk);

            if (ReciveMessage().IsEmpty)
                return;
            
        }
    }
}
