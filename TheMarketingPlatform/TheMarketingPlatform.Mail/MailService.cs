using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Mail
{
    public class MailService
    {
        private List<ImapClient> imapClients;

        public MailService()
        {
            imapClients = new List<ImapClient>();
        }
        public MailService(List<IMailClientSettings> settings)
        {
            foreach (var imapSetting in settings.Where(s => s.Type == MailClientType.Imap))
            {
                var client = new ImapClient();
                client.Connect(imapSetting.Host, imapSetting.Port, imapSetting.UseSsl);
                client.Authenticate(imapSetting.UserName, imapSetting.Password);
                imapClients.Add(client);
            }            
        }
    }
}
