using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core.JSON
{
    public class MailAccount
    {
        public int Id { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool SSL { get; set; }
        public string Type { get; set; }

        public static byte[] Serialize(MailAccount mailAccount)=>
          Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mailAccount));

        public static MailAccount Deserialize(byte[] data) =>
            JsonConvert.DeserializeObject<MailAccount>(Encoding.UTF8.GetString(data));
        
    }
}
