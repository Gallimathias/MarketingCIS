using CommandManagementSystem.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.Network;

namespace TheMarketingPlatform.Service.Commands
{
    public static class OneTimeCommands
    {
        internal static SettingsHandler SettingsHandler { get; set; }

        [OneTimeCommand("test2")]
        public static object Test(KeyValuePair<TcpConnection, byte[]> args)
        {
            args.Key.Send(new NetworkMessage("+OK", Encoding.UTF8.GetBytes("Mensch a wasser wär jedzd ebbes gschickds")));
            return args;
        }
        
    }
}
