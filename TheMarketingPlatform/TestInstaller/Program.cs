using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.JSON;

namespace TestInstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            var val = Registry.GetValue(
                $@"HKEY_LOCAL_MACHINE\SOFTWARE\Gallimathias\TheMarketingPlatform\Service", "Config", null);

            if (val == null)
                Registry.SetValue(
                    $@"HKEY_LOCAL_MACHINE\SOFTWARE\Gallimathias\TheMarketingPlatform\Service", 
                    "Config",
                    @"C:\Temp\Main.config");

            var conf = new Config();
            conf.Settings.Add("LUISAppId", "");
            conf.Settings.Add("LUISAppKey", "");
            conf.Settings.Add("ServerPort", 33333);
            conf.Settings.Add("MailServiceHandlerPeriod", 6000);
            conf.Settings.Add("MailServicePeriod", 6000);
            conf.Settings.Add("DatabaseConnectionString", "");

            File.WriteAllText(@"C:\Temp\Main.config", JsonConvert.SerializeObject(conf, Formatting.Indented));
                
        }
    }
}
