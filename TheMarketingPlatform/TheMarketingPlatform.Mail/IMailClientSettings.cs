using System.Net;
using System.Text;
using MailKit.Security;

namespace TheMarketingPlatform.Mail
{
    public interface IMailClientSettings
    {
        MailClientType Type { get; set; }
        SecureSocketOptions UseSsl { get; set; }
        int Port { get; set; }
        string Host { get; set; }
        string Password { get; set; }
        string UserName { get; set; }
    }
}