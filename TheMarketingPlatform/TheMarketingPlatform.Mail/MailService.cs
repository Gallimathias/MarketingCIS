using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Mail
{
    /// <summary>
    /// This service call mail accounts
    /// </summary>
    public class MailService
    {
        /// <summary>
        /// Handles folder events
        /// </summary>
        /// <param name="sender">the throwing folder</param>
        public delegate void FolderEventHandler(IMailFolder sender);
        /// <summary>
        /// Thrown if folder count change
        /// </summary>
        public event FolderEventHandler FolderCountChanged;

        private Dictionary<ImapClient, ImapClientSetting> imapClients;
        private List<IMailFolder> subscribedFolders;

        /// <summary>
        /// This service call mail accounts
        /// </summary>
        public MailService()
        {
            imapClients = new Dictionary<ImapClient, ImapClientSetting>();
            subscribedFolders = new List<IMailFolder>();
        }
        /// <summary>
        /// This service call mail accounts
        /// </summary>       
        public MailService(List<IMailClientSettings> settings) : this()
        {
            Initialize(settings);
            Subscribe();
        }

        /// <summary>
        /// Subscribed all folders from settings
        /// </summary>
        public void Subscribe()
        {
            foreach (var imapClient in imapClients)
            {
                var settingFolder = imapClient.Value.Folder ?? new List<string>();
                if (settingFolder == null || settingFolder.Count < 1)
                    settingFolder.Add(imapClient.Key.Inbox.Name);

                

                foreach (var folderName in settingFolder)
                {
                    var subFolder = imapClient.Key.GetFolder(folderName);
                    subFolder.CountChanged += (s, e) => FolderCountChanged?.Invoke((IMailFolder)s);
                    subFolder.Open(FolderAccess.ReadOnly);
                    subscribedFolders.Add(subFolder);
                }
            }
        }

        /// <summary>
        /// Get all Mails
        /// </summary>
        /// <returns>return new mails</returns>
        public List<MimeMessage> GetMails()
        {
            var tmpList = new List<MimeMessage>();
            foreach (var folder in subscribedFolders)
            {
                foreach (var mail in folder.ToArray())
                {
                    tmpList.Add(mail);
                }
            }
            return tmpList;
        }
        /// <summary>
        /// Get all Mails where older then a specific datetime
        /// </summary>
        /// <param name="lastMessageDate">the datetime of the last message</param>
        /// <returns>Returns new messages</returns>
        public List<MimeMessage> GetMails(DateTimeOffset lastMessageDate) =>
            GetMails().Where(m => m.Date > lastMessageDate).ToList();

        /// <summary>
        /// init new call process
        /// </summary>
        /// <param name="settings">specific client settings</param>
        public void Initialize(List<IMailClientSettings> settings)
        {
            foreach (var imapSetting in settings.Where(s => s.Type == MailClientType.Imap))
            {
                var client = new ImapClient();
                client.Connect(imapSetting.Host, imapSetting.Port, imapSetting.UseSsl);
                client.Authenticate(imapSetting.UserName, imapSetting.Password);
                imapClients.Add(client, imapSetting as ImapClientSetting);
            }            
        }
    }
}
