using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TheMarketingPlatform.Client;
using TheMarketingPlatform.Client.Commands;

namespace TheMarketingPlatform
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private TcpClient client;
        private SettingsHandler settingsHandler;
        private TrayIcon trayIcon;
        private Timer timer;
        private ClientCommandManager commandManager;

        public App()
        {
            settingsHandler = new SettingsHandler();
            commandManager = new ClientCommandManager(settingsHandler);
            
            if ((bool)settingsHandler["Initializes"])
                Task.Run(() => InitializeClient(null));

        }

        private void InitializeClient(object state)
        {
            short connectionFailure = 0;
            bool connectionFail = true;

            while (connectionFailure < (long)settingsHandler["MaxConnectionRetries"]
                && connectionFail)
            {
                try
                {
                    client = new TcpClient((string)settingsHandler["Host"], (int)(long)settingsHandler["Port"]);
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

            if (connectionFail)
            {
                timer = new Timer(InitializeClient, null, 3000, 3000);
            }
            else
            {
                timer.Dispose();
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
