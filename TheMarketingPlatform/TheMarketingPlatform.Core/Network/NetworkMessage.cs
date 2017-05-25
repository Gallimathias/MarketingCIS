using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace TheMarketingPlatform.Core.Network
{
    /// <summary>
    /// A message to send between server and client
    /// </summary>
    public class NetworkMessage
    {
        /// <summary>
        /// The current message tag for a command dispatch
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// The data payload. Is not for Json
        /// </summary>
        [JsonIgnore]
        public byte[] Payload { get; set; }
        /// <summary>
        /// A Message is empty if the tag is null or empty or contains a plus and the payload is 0
        /// </summary>
        public bool IsEmpty => (string.IsNullOrEmpty(Tag) || Tag == "+") && Payload.Length == 0;

        /// <summary>
        /// A message to send between server and client
        /// </summary>
        /// <param name="tag">The network tag</param>
        /// <param name="payload">The data to send</param>
        public NetworkMessage(string tag, byte[] payload)
        {
            Tag = tag;
            Payload = payload;
        }

        /// <summary>
        /// Gets a default ok to send
        /// </summary>
        public static NetworkMessage DefaultOk => new NetworkMessage("+OK", new byte[0]);
        /// <summary>
        /// Gets a default error to send
        /// </summary>
        public static NetworkMessage DefaultError => new NetworkMessage("+ERR", new byte[0]);
        /// <summary>
        /// Gets a empty message to send
        /// </summary>
        public static NetworkMessage EmptyMessage => new NetworkMessage("+", new byte[0]);

        /// <summary>
        /// Serialize Message with payload to byte array
        /// </summary>
        /// <param name="message">The message to serialize</param>
        /// <returns>The message as byte array with payload</returns>
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

        /// <summary>
        /// Deserialize a byte array from a message with payload to message
        /// </summary>
        /// <param name="data">A byte array from a message with payload</param>
        /// <returns>A New message with Tag and Payload</returns>
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
