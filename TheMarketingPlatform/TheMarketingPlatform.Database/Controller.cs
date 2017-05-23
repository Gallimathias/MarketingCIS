using MimeKit;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.JSON;
using TheMarketingPlatform.Mail;

namespace TheMarketingPlatform.Database
{
    public class Controller
    {
        public List<IMailClientSettings> MailClientSettings { get; set; }

        MainDatabaseContext databaseContext;

        public Controller(string connection)
        {
            databaseContext = new MainDatabaseContext(connection);
        }

        public Mail GetLastMessage() =>
            databaseContext.GetTable<Mail>().OrderByDescending(m => m.TimeStamp).FirstOrDefault();

        public int Insert(MimeMessage mimeMessage)
        {
            var mail = new Mail()
            {
                Body = mimeMessage.TextBody,
                Subject = mimeMessage.Subject,
                TimeStamp = mimeMessage.Date
            };

            databaseContext.GetTable<Mail>().InsertOnSubmit(mail);
            databaseContext.SubmitChanges();

            var mailAddresses = new List<MailAddresses>();
            var dictonary = new Dictionary<AddressType, InternetAddressList>
            {
                { AddressType.From, mimeMessage.From },
                { AddressType.To, mimeMessage.To },
                { AddressType.CC, mimeMessage.Cc },
                { AddressType.BCC, mimeMessage.Bcc }
            };

            foreach (var type in dictonary)
            {
                foreach (var adress in type.Value)
                {
                    mailAddresses.Add(new MailAddresses()
                    {
                        MailAddress = adress.Name,
                        MailId = mail.Id,
                        Type = new[] { (byte)type.Key }
                    });
                }
            }

            databaseContext.GetTable<MailAddresses>().InsertAllOnSubmit(mailAddresses);
            databaseContext.SubmitChanges();

            return mail.Id;
        }
        public void Insert(Response response, int messageid)
        {
            throw new NotImplementedException();
        }
    }
}
