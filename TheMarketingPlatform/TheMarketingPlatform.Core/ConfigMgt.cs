﻿using TheMarketingPlatform.Core.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core
{
    public static class ConfigMgt
    {
        private static Config config;
        private static FileInfo configFile;

        public static string LuisAppId { get { return config.luisAppId; } internal set { config.luisAppId = value; } }
        public static string LuisAppKey { get { return config.luisAppKey; } internal set { config.luisAppKey = value; } }

        public static string MailHost { get { return config.mailHost; } internal set { config.mailHost = value; } }
        public static int MailPort { get { return config.mailPort; } internal set { config.mailPort = value; } }
        public static bool MailSSL { get { return config.mailSSL; } internal set { config.mailSSL = value; } }

        public static void GetConfigFrom(string fullName)
        {
            configFile = new FileInfo(fullName);
            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(fullName));
        }

        public static void ApplyChangesToFile() =>
            File.WriteAllText(configFile.FullName, JsonConvert.SerializeObject(config, Formatting.Indented));

    }
}