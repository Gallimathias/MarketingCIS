using System.Net;
using System.Text;
using MailKit.Security;

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
    }
}