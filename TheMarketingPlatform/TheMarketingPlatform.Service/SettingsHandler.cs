using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core;
using TheMarketingPlatform.Core.JSON;
using TheMarketingPlatform.Database;

namespace TheMarketingPlatform.Service
{
    class SettingsHandler : ConfigManagement
    {
        public Controller DatabaseController { get; private set; }
        internal Config ServerConfig => Config;

        public bool NewMessage { get; internal set; }

        public SettingsHandler()
        {
            var path = Registry.GetValue(
                $@"HKEY_LOCAL_MACHINE\SOFTWARE\Gallimathias\TheMarketingPlatform\Service", "Config", null)
                as string;
            
            if (string.IsNullOrEmpty(path))
                throw new Exception("Configuration file does not exist");

            var loadingResult = Load(path);

            if (!loadingResult.loaded)
                throw new Exception("Load file failed", loadingResult.exception);

            DatabaseController = new Controller((string)this["DatabaseConnectionString"]);
        }

        internal void Update(Config config)
        {
            foreach (var setting in config.Settings)
                this[setting.Key] = setting.Value;

            ApplyChangesToFile();
        }
    }
}
