using MailKit;
using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImapTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ImapClient();

            
            client.Connect("imap.web.de", 0, true);
            client.Authenticate("udo.test@web.de", "udo.test");
            client.Inbox.MessagesArrived += Inbox_MessagesArrived;
            client.Inbox.Open(FolderAccess.ReadOnly);
            var mails = client.Inbox.ToArray();
            var list = new List<int>();
            for (int i = 0; i < mails.Length; i++)
            {
                list.Add(i);
               
            }

            var fetched = client.Inbox.Fetch(list, MessageSummaryItems.Flags | MessageSummaryItems.GMailLabels);


            client.Inbox.Subscribe();

            Console.ReadKey();
        }

        private static void Inbox_MessagesArrived(object sender, MessagesArrivedEventArgs e)
        {

        }
    }
}
