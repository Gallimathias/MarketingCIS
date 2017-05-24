using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Service
{
    static class Program
    {
        static ManualResetEvent manualResetEvent;

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        static void Main()
        {

            manualResetEvent = new ManualResetEvent(false);
            var settingsHandler = new SettingsHandler();

            var mailService = new MailService(settingsHandler);
            var lUISService = new LUISService(settingsHandler);

            mailService.OnNewMessages += (s, m) => lUISService.HandleMessages(m);
            mailService.Start();

            manualResetEvent.WaitOne();
        }
    }
}
