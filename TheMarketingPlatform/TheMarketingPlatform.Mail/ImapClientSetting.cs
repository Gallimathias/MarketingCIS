using System;
using MailKit.Security;
using MailKit;
using System.Collections.Generic;

namespace TheMarketingPlatform.Mail
{
    public class ImapClientSetting : IMailClientSettings
    {
        public MailClientType Type => MailClientType.Imap;
        public bool UseSsl { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public List<string> Folder { get; set; }
    }
}