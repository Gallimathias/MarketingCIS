using MailKit;
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
        private Dictionary<ImapClient, ImapClientSetting> imapClients;

        public MailService()
        {
            imapClients = new Dictionary<ImapClient, ImapClientSetting>();
        }
        public MailService(List<IMailClientSettings> settings)
        {
            Initialize(settings);
        }

        public void Subscibe()
        {
            foreach (var imapClient in imapClients)
            {
                var subscribedFolders = imapClient.Value.Folder;
                if (subscribedFolders == null || subscribedFolders.Length < 1)
                    subscribedFolders = new[] { imapClient.Key.Inbox };

                foreach (var folder in subscribedFolders)
                {
                    folder.Subscribe();
                    folder.MessagesArrived += Folder_MessagesArrived;
                }
            }
        }

        public void Initialize(List<IMailClientSettings> settings)
        {
            foreach (var imapSetting in settings.Where(s => s.Type == MailClientType.Imap))
            {
                var client = new ImapClient();
                client.Connect(imapSetting.Host, imapSetting.Port, imapSetting.UseSsl);
                client.Authenticate(imapSetting.UserName, imapSetting.Password);
                imapClients.Add(client, imapSetting as ImapClientSetting);
            }

            Subscibe();
        }
        
        private void Folder_MessagesArrived(object sender, MessagesArrivedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
