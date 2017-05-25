using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core;

namespace TheMarketingPlatfom.Client
{
    public class SettingsHandler : ConfigManagement
    {
        public Client Client { get => client; set {
                client = value;
                client.OnConnect += (s) => ClientIsReady?.Invoke(this, client);
            } }
        private Client client;

        public event EventHandler<Client> ClientIsReady;
        public SettingsHandler()
        {
            var path = Registry.GetValue(
                $@"HKEY_CURRENT_USER\SOFTWARE\Gallimathias\TheMarketingPlatform\Main", "Config", null)
                as string;

            if (string.IsNullOrEmpty(path))
                throw new Exception("Configuration file does not exist");

            var loadingResult = Load(path);

            if (!loadingResult.loaded)
                throw new Exception("Load file failed", loadingResult.exception);
        }

    }
}
