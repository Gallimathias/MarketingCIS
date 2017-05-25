using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core.JSON
{
    /// <summary>
    /// Represents a config file for json
    /// </summary>
    public class Config
    {
        /// <summary>
        /// the main settings dictionary
        /// </summary>
        public Dictionary<string, object> Settings { get; set; }

        /// <summary>
        /// Represents a config file for json
        /// </summary>
        public Config()
        {
            Settings = new Dictionary<string, object>();
        }

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
