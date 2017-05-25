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
        [OneTimeCommand("disconnect")]
        public static object Disconnect(KeyValuePair<TcpConnection, byte[]> args)
        {
            args.Key.Disconnect();
            return args;
        }

        [OneTimeCommand("newmessage")]
        public static object Newmessage(KeyValuePair<TcpConnection, byte[]> args)
        {
            if (SettingsHandler.NewMessage)
            {
                args.Key.Send(new NetworkMessage("newmessage", new byte[0]));
                SettingsHandler.NewMessage = false;
            }
            else
            {
                args.Key.Send(NetworkMessage.EmptyMessage);
            }
            return args;
        }
    }
}
