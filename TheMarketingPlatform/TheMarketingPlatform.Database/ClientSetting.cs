using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Mail;

namespace TheMarketingPlatform.Database
{
    public class ClientSetting : IMailClientSettings
    {
        public MailClientType Type { get; set; }

        public bool UseSsl { get; set; }

        public int Port { get; set; }

        public string Host { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }
    }
}
