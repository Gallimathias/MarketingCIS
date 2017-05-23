using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core.JSON
{
    internal class Config
    {
        public string Version { get; set; }
        public string ProductName { get; set; }
        public string LuisAppId { get; set; }
        public string LuisAppKey { get; set; }
        public string DatabaseConnectionString { get; set; }
        public int MailServicePeriod { get; set; }


        internal IEnumerable<KeyValuePair<string, object>> ToList()
        {
            var tmpList = new List<KeyValuePair<string, object>>();

            foreach (var propertie in GetType().GetProperties())
                tmpList.Add(
                    new KeyValuePair<string, object>(propertie.Name, propertie.GetValue(this)));

            return tmpList;
        }
    }
}
