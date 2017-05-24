using CommandManagementSystem;
using CommandManagementSystem.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Service
{
    [CommandManager("ServiceCommandManager","TheMarketingPlatform.Service.Commands")]
    internal class ServiceCommandManager : CommandManager<string, byte[], bool>
    {
    }
}
