using MimeKit;
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
    internal class MailService
    {
        public delegate void MailEventHandler(object sender, params MimeMessage[] message);
        public event MailEventHandler OnNewMessages;

        public SettingsHandler SettingsHandler { get; private set; }

        private Mail.MailService mailService;
        private Timer timer;

        public MailService(SettingsHandler settingsHandler)
        {
            mailService = new Mail.MailService(SettingsHandler.DatabaseController.MailClientSettings);
            SettingsHandler = settingsHandler;
        }
        private void Process(object state)
        {
            var lastMessage = SettingsHandler.DatabaseController.GetLastMessage();
            var messages = mailService.GetMails(lastMessage.TimeStamp);

            OnNewMessages?.Invoke(this, messages.ToArray());
        }

        private void InitializeTimer() => timer = new Timer(Process, null, 0, (int)SettingsHandler["MailServicePeriod"]);
    }
}
