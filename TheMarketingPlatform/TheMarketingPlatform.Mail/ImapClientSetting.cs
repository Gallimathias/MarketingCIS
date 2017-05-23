using System;
using MailKit.Security;
using MailKit;

namespace TheMarketingPlatform.Mail
{
    internal class ImapClientSetting : IMailClientSettings
    {
        public MailClientType Type => MailClientType.Imap;
        public bool UseSsl { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public IMailFolder[] Folder { get; set; }
    }
}