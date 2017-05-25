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
        internal static SettingsHandler SettingsHandler { get; set; }
        
        [OneTimeCommand("getserverconfig")]
        public static object GetServerConfig(KeyValuePair<TcpConnection, byte[]> args)
        {
            args.Key.Send(new NetworkMessage("serverconfig",
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(SettingsHandler.ServerConfig))));
            return args;
        }

        [OneTimeCommand("setserverconfig")]
        public static object SetServerConfig(KeyValuePair<TcpConnection, byte[]> args)
        {
            var config = JsonConvert.DeserializeObject<Config>(Encoding.UTF8.GetString(args.Value));
            SettingsHandler.Update(config);
            args.Key.Send(NetworkMessage.DefaultOk);
            return args;
        }
  
    }
}
