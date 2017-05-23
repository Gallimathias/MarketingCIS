using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core
{
    public static class MailMgt
    {
        private static List<ImapClient> imapClients;

        public static void LoadMail()
        {

        }

        public static void GetAllMails()
        {
            foreach (var imapClient in imapClients)
            {
            }
        }
        
    }
}
