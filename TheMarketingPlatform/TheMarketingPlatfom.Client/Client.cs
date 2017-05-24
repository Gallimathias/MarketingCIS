using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.Network;

namespace TheMarketingPlatfom.Client
{
    public class Client : TcpConnection
    {
        public Client(string host, int port) : base(host, port)
        {
        }

        public void Connect()
        {
            Send(NetworkMessage.DefaultOk);
            ReciveMessage();

            using (var ecd = new ECDiffieHellmanCng() {
                KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash,
                HashAlgorithm = CngAlgorithm.Sha256
            })
            {
                Send(new NetworkMessage(null, ecd.PublicKey.ToByteArray()));
                var publicKey = ReciveMessage().Payload;

                Key = ecd.DeriveKeyMaterial(CngKey.Import(publicKey, CngKeyBlobFormat.EccPublicBlob));
            }

        }
    }
}
