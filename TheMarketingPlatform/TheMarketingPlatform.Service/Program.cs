using CommandManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Service
{
    static class Program
    {
        static ManualResetEvent manualResetEvent;
        static SettingsHandler settingsHandler;
        static ServiceCommandManager commandManager;
        static MailService mailService;
        static LUISService lUISService;
        static TcpServer tcpServer;

        static void Main()
        {

            manualResetEvent = new ManualResetEvent(false);
            settingsHandler = new SettingsHandler();

            //commandManager = new DefaultCommandManager("TheMarketingPlatform.Service.Commands");
            commandManager = new ServiceCommandManager();


            mailService = new MailService(settingsHandler);
            lUISService = new LUISService(settingsHandler);
            tcpServer = new TcpServer(IPAddress.Any, (int)(long)settingsHandler["ServerPort"])
            {
                SettingsHandler = settingsHandler,
                CommandManager = commandManager
            };

            mailService.OnNewMessages += (s, m) => lUISService.HandleMessages(m);
            mailService.Start();

            manualResetEvent.WaitOne();
        }
    }
}
