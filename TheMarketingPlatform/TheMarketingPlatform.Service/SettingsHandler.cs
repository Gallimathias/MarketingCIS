using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core;

namespace TheMarketingPlatform.Service
{
    class SettingsHandler
    {
        public ConfigManagement MainConfigManagement { get; private set; }

        public SettingsHandler()
        {
          var path =  Registry.GetValue(
              @"HKEY_LOCAL_MACHINE\SOFTWARE\Gallimathias\TheMarketingPlatform\Main", "Config", null)
              as string;

            if (string.IsNullOrEmpty(path))
                throw new Exception("Configuration file does not exist");

            MainConfigManagement = new ConfigManagement();
            var loadingResult = MainConfigManagement.Load(path);

            if (!loadingResult.Item1)
                throw new Exception("Load file failed", loadingResult.Item2);
        }

        internal int GetValue(string key)
        {
            throw new NotImplementedException();
        }
    }
}
