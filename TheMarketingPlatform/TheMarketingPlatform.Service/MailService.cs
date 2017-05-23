using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mail = TheMarketingPlatform.Mail;

namespace TheMarketingPlatform.Service
{
    internal class MailService : ServiceBase
    {
        public SettingsHandler SettingsHandler { get; private set; }

        private Mail.MailService mailService;
        private ManualResetEvent manualResetEvent;
        private Timer timer;

        public MailService(SettingsHandler settingsHandler)
        {
            mailService = new Mail.MailService();
            SettingsHandler = settingsHandler;
            manualResetEvent = new ManualResetEvent(true);
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer(Process, null, 0, (int)SettingsHandler["MailServicePeriod"]);
            base.OnStart(args);
        }

        protected override void OnPause()
        {
            manualResetEvent.Reset();
            base.OnPause();
        }

        protected override void OnContinue()
        {
            manualResetEvent.Set();
            base.OnContinue();
        }

        protected override void OnStop()
        {
            timer.Dispose();
            base.OnStop();
        }

        private void Process(object state)
        {
            manualResetEvent.WaitOne();
        }
    }
}
