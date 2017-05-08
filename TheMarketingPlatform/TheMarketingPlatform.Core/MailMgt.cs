using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using TheMarketingPlatform.Core.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core
{
    public static class MailMgt
    {
        private static ConnectionType connectionType;
        private static MailClient mailClient;

        public static void ConnectToServer(ConnectionType connectionType)
        {
            switch (connectionType)
            {
                case ConnectionType.IMAP:
                    mailClient = new MailClient<ImapClient>(ConfigMgt.MailHost, ConfigMgt.MailPort, ConfigMgt.MailSSL);
                    break;
                case ConnectionType.POP3:
                    mailClient = new MailClient<Pop3Client>(ConfigMgt.MailHost, ConfigMgt.MailPort, ConfigMgt.MailSSL); 
                    break;
                case ConnectionType.SMTP:
                    mailClient = new MailClient<SmtpClient>(ConfigMgt.MailHost, ConfigMgt.MailPort, ConfigMgt.MailSSL);
                    break;
                default:
                    break;
            }
            
        }
    }
}
