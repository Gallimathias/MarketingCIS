﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core;
using TheMarketingPlatform.Database;

namespace TheMarketingPlatform.Service
{
    class SettingsHandler : ConfigManagement
    {
        public Controller DatabaseController { get; private set; }

        public SettingsHandler()
        {
            var path = Registry.GetValue(
                @"HKEY_LOCAL_MACHINE\SOFTWARE\Gallimathias\TheMarketingPlatform\Main", "Config", null)
                as string;
            
            if (string.IsNullOrEmpty(path))
                throw new Exception("Configuration file does not exist");

            var loadingResult = Load(path);

            if (!loadingResult.loaded)
                throw new Exception("Load file failed", loadingResult.exception);

            DatabaseController = new Controller((string)this["DatabaseConnectionString"]);
        }
    }
}
