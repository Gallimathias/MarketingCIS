using CommandManagementSystem;
using CommandManagementSystem.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Service.Commands
{
    [Command("test")]
    class TestCommand : Command<byte[], object>
    {
        public override object Main(byte[] arg)
        {
            return base.Main(arg);
        }

        public void Register()
        {
            Registration();
        }
    }
}
