using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit;

namespace MarketingCIS.Core.Mail
{
    internal abstract class MailClient
    {
        public Type ClientType { get; private set; }
        protected object mailClient;

        public MailClient(Type type)
        {
            ClientType = type;
        }

    }

    internal class MailClient<T> : MailClient
    {
        private new T mailClient { get { return (T)base.mailClient; } set { base.mailClient = value; } }

        public MailClient(string host, int port, bool useSSL) : base(typeof(T))
        {
            mailClient = (T)Activator.CreateInstance(typeof(T), host, port, useSSL);
        }
    }
}
