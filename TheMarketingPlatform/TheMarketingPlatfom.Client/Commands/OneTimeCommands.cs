using CommandManagementSystem.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.Network;

namespace TheMarketingPlatform.Client.Commands
{
    public static class OneTimeCommands
    {
        internal static SettingsHandler SettingsHandler { get; set; }

        [OneTimeCommand("test2")]
        public static object Test(KeyValuePair<TcpClient, byte[]> args)
        {
            return args;
        }
    }
}
