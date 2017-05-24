using System.Net;
using System.Text;
using MailKit.Security;
using System.Collections.Generic;

namespace TheMarketingPlatform.Mail
{
    public interface IMailClientSettings
    {
        MailClientType Type { get; }
        bool UseSsl { get; }
        int Port { get; }
        string Host { get; }
        string Password { get; }
        string UserName { get; }
        List<string> Folder { get; }
    }
}