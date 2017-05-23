using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core;

namespace TheMarketingPlatform.Service
{
    class SettingsHandler : ConfigManagement
    {
        public SettingsHandler()
        {
            var path = Registry.GetValue(
                @"HKEY_LOCAL_MACHINE\SOFTWARE\Gallimathias\TheMarketingPlatform\Main", "Config", null)
                as string;

            if (string.IsNullOrEmpty(path))
                throw new Exception("Configuration file does not exist");

            var loadingResult = Load(path);

            if (!loadingResult.Item1)
                throw new Exception("Load file failed", loadingResult.Item2);
        }
    }
}
