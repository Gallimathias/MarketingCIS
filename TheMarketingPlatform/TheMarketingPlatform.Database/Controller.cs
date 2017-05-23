using MimeKit;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
    }
}
