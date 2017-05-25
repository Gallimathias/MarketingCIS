using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core;

namespace TheMarketingPlatform.Client
{
    public class SettingsHandler : ConfigManagement
    {
        public TcpClient Client
        {
            get => client; set
            {
                client = value;
                client.OnConnect += (s) => ClientIsReady?.Invoke(this, client);
            }
        }

        public ClientCommandManager CommandManager { get; internal set; }

        private TcpClient client;

        public event EventHandler<TcpClient> ClientIsReady;
        public event EventHandler<Notification> OnNotify;

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

        public void Notifycate(object sender, Notification notification) => OnNotify?.Invoke(sender, notification);


    }
}
