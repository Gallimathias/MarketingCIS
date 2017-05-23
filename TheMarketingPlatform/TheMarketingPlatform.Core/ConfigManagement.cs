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
    public class ConfigManagement : Dictionary<string, object>
    {
        private Config config;
        private FileInfo configFile;

        public (bool, Exception) Load(string fullName)
        {
            configFile = new FileInfo(fullName);

            if (!configFile.Exists)
                return (false, new FileNotFoundException());

            try
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(fullName));

                foreach (var setting in config.ToList())
                    Add(setting.Key, setting.Value);

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
