using CommandManagementSystem;
using CommandManagementSystem.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.Network;

namespace TheMarketingPlatform.Service.Commands
{
    [Command("test")]
    class TestCommand : Command<KeyValuePair<TcpConnection, byte[]>, object>
    {
        SettingsHandler settingsHandler;
        public TestCommand(SettingsHandler settingsHandler) : base()
        {
            this.settingsHandler = settingsHandler;
        }

        public override object Main(KeyValuePair<TcpConnection, byte[]> arg)
        {
            NextFunction = null;
            return arg;
        }
        
    }
}
