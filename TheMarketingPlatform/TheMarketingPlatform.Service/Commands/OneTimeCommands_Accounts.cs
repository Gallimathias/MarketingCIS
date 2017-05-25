using CommandManagementSystem.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.JSON;
using TheMarketingPlatform.Core.Network;
using TheMarketingPlatform.Mail;

namespace TheMarketingPlatform.Service.Commands
{
    public static partial class OneTimeCommands
    {
        [OneTimeCommand("getmailaccountscount")]
        public static object GetMailAccountsCount(KeyValuePair<TcpConnection, byte[]> args)
        {
            var count = JsonConvert.DeserializeObject<Config>(Encoding.UTF8.GetString(args.Value));
            args.Key.Send(new NetworkMessage("mailaccountscount", BitConverter.GetBytes(
                SettingsHandler.DatabaseController.MailAccountsCount)));
            return args;
        }

        [OneTimeCommand("getmailaccounts")]
        public static object GetMailAccounts(KeyValuePair<TcpConnection, byte[]> args)
        {
            var mailAccounts = SettingsHandler.DatabaseController.MailAccounts;
            var list = new List<byte[]>();

            foreach (var mailAccount in mailAccounts)
            {
                list.Add(MailAccount.Serialize(new MailAccount()
                {
                    Id = mailAccount.Id,
                    Host = mailAccount.Host,
                    Password = mailAccount.Password,
                    Port = mailAccount.Port,
                    SSL = mailAccount.UseSsl,
                    Type = ((MailClientType)mailAccount.Type.ToArray()[0]).ToString(),
                    Username = mailAccount.Username
                }));
            }

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(list.Count);
                    foreach (var byteArray in list)
                    {
                        writer.Write(byteArray.Length);
                        writer.Write(byteArray);
                    }
                }
                args.Key.Send(new NetworkMessage("mailaccounts", stream.ToArray()));
            }


            return args;
        }

        [OneTimeCommand("addmailaccounts")]
        public static object AddMailAccounts(KeyValuePair<TcpConnection, byte[]> args)
        {
            var list = new List<MailAccount>();
            foreach (var account in TcpConnection.GetListFromData(args.Value))
            {
                list.Add(MailAccount.Deserialize(account));
            }

            SettingsHandler.DatabaseController.AddMailAccounts(list);
            return args;
        }

        [OneTimeCommand("removemailaccounts")]
        public static object RemoveMailAccounts(KeyValuePair<TcpConnection, byte[]> args)
        {
            var list = new List<MailAccount>();
            foreach (var account in TcpConnection.GetListFromData(args.Value))
            {
                list.Add(MailAccount.Deserialize(account));
            }

            SettingsHandler.DatabaseController.RemoveMailAccounts(list);
            return args;
        }
    }
}
