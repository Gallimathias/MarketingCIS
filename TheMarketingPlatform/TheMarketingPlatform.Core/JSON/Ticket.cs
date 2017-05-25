using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core.JSON
{
    public class Ticket
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
        public string Intent { get; set; }
        public double Score { get; set; }

        public static byte[] Serialize(Ticket ticket) =>
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ticket));

        public static Ticket Deserialize(byte[] data) =>
            JsonConvert.DeserializeObject<Ticket>(Encoding.UTF8.GetString(data));
    }
}
