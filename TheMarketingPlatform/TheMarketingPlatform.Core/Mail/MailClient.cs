using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;

namespace TheMarketingPlatform.Core.Mail
{
    public class MailClient
    {
        public string Host { get; private set; }
        public int ImapPort { get; private set; }
        public int SmtpPort { get; private set; }
        public bool UseSSL { get; private set; }

        public IMailFolder Inbox => imapClient.Inbox;

        private ImapClient imapClient;
        private SmtpClient smtpClient;

        public MailClient()
        {
            imapClient = new ImapClient();
            smtpClient = new SmtpClient();
        }

        public void Connect(string host, int port, bool useSSL)
        {
            Host = host;
            ImapPort = port;
            UseSSL = useSSL;

            imapClient.Connect(host, port, useSSL);
            smtpClient.Connect(host, port, useSSL);
        }

        public void Authenticate(string username, string password)
        {
            imapClient.Authenticate(username, password);
            smtpClient.Authenticate(username, password);
        }
    }
}
