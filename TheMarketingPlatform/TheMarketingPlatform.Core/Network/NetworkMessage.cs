using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace TheMarketingPlatform.Core.Network
{
    public class NetworkMessage
    {
        public string Tag { get; set; }
        [JsonIgnore]
        public byte[] Payload { get; set; }
        
        public NetworkMessage(string tag, byte[] payload)
        {
            Tag = tag;
            Payload = payload;
        }

        public static NetworkMessage DefaultOk => new NetworkMessage("+OK", new byte[0]);
        public static NetworkMessage DefaultError => new NetworkMessage("+ERR", new byte[0]);

        public static byte[] Serialize(NetworkMessage message)
        {
            var head = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(head.Length);
                    writer.Write(head);
                    writer.Write(message.Payload.Length);
                    writer.Write(message.Payload);
                }

                return stream.ToArray();
            }
        }

        public static NetworkMessage Deserialize(byte[] data)
        {
            NetworkMessage message;

            using (var stream = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(stream))
                {
                    message = JsonConvert.DeserializeObject<NetworkMessage>(
                        Encoding.UTF8.GetString(
                            reader.ReadBytes(reader.ReadInt32())));

                    message.Payload = reader.ReadBytes(reader.ReadInt32());
                }
            }

            return message;
        }

    }
}
