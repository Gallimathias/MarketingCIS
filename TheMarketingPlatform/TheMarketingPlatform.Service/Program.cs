using CommandManagementSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheMarketingPlatform.Service.Commands;

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
            
            commandManager = new ServiceCommandManager(settingsHandler);
            OneTimeCommands.SettingsHandler = settingsHandler;


            mailService = new MailService(settingsHandler);
            lUISService = new LUISService(settingsHandler);
            tcpServer = new TcpServer(IPAddress.Any, (int)(long)settingsHandler["ServerPort"])
            {
                SettingsHandler = settingsHandler,
                CommandManager = commandManager
            };
            tcpServer.Start();
            mailService.OnNewMessages += (s, m) => lUISService.HandleMessages(m);
            mailService.Start();

            manualResetEvent.WaitOne();
        }
    }
}
