using MarketingCIS.Core.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingCIS.Core
{
    static class ConfigMgt
    {
        private static Config config;
        private static FileInfo configFile;

        public static void GetConfigFrom(string fullName)
        {
            configFile = new FileInfo(fullName);
            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(fullName));
        }

        public static void ApplyChangesToFile() =>
            File.WriteAllText(configFile.FullName, JsonConvert.SerializeObject(config, Formatting.Indented));

    }
}
