using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TheMarketingPlatfom.Client;

namespace TheMarketingPlatform
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private Client client;
        private SettingsHandler settingsHandler;
        private TrayIcon trayIcon;

        public App()
        {
            settingsHandler = new SettingsHandler();

            if ((bool)settingsHandler["Initializes"])
                Task.Run(() => InitializeClient());
        }

        private void InitializeClient()
        {
            short connectionFailure = 0;
            bool connectionFail = true;

            while (connectionFailure < (long)settingsHandler["MaxConnectionRetries"]
                && connectionFail)
            {
                try
                {
                    client = new Client((string)settingsHandler["Host"], (int)(long)settingsHandler["Port"]);
                    client.Connect();
                    connectionFail = false;
                    settingsHandler.Client = client;
                }
                catch (Exception)
                {
                    connectionFailure++;
                    connectionFail = true;
                }
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            trayIcon = new TrayIcon(settingsHandler);
            base.OnStartup(e);
        }


    }


}
