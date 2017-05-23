using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Service
{
    class SettingsHandler
    {
        public SettingsHandler()
        {
            Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Gallimathias\TheMarketingPlatform\Main", "", null);
        }
    }
}
