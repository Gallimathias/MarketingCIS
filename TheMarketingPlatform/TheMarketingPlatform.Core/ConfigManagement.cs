using TheMarketingPlatform.Core.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core
{
    public class ConfigManagement
    {
        private Config config;
        private FileInfo configFile;

        public string LuisAppId { get { return config.LuisAppId; } set { config.LuisAppId = value; } }
        public string LuisAppKey { get { return config.LuisAppKey; } set { config.LuisAppKey = value; } }


        public (bool, Exception) Load(string fullName)
        {
            configFile = new FileInfo(fullName);

            if (!configFile.Exists)
                return (false, new FileNotFoundException());

            try
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(fullName));
            }
            catch (Exception ex)
            {
                return (false, ex);
            }

            return (true, null);
        }

        public void ApplyChangesToFile() =>
            File.WriteAllText(configFile.FullName, JsonConvert.SerializeObject(config, Formatting.Indented));

    }
}
