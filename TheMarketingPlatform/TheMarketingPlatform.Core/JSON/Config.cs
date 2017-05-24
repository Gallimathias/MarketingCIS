using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core.JSON
{
    public class Config
    {
        public Dictionary<string, object> Settings { get; set; }


        internal IEnumerable<KeyValuePair<string, object>> ToList()
        {
            var tmpList = new List<KeyValuePair<string, object>>();

            foreach (var setting in Settings)
                tmpList.Add(
                    new KeyValuePair<string, object>(setting.Key, setting.Value));

            return tmpList;
        }
    }
}
