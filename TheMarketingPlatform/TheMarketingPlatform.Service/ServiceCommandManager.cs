using CommandManagementSystem;
using CommandManagementSystem.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Service
{
    [CommandManager("ServiceCommandManager", "TheMarketingPlatform.Service.Commands")]
    internal class ServiceCommandManager : CommandManager<string, byte[], object>
    {
        public ServiceCommandManager() : base()
        {

        }

        public override void Initialize()
        {
            var commandNamespace = GetType().GetCustomAttribute<CommandManagerAttribute>().CommandNamespaces;

            var commands = Assembly.GetExecutingAssembly().GetTypes().Where(
                t => t.GetCustomAttribute<CommandAttribute>() != null && commandNamespace.Contains(t.Namespace)).ToList();

            foreach (var command in commands)
            {
                command.GetMethod("Register",BindingFlags.Public | BindingFlags.Static)?.Invoke(null, null);
                commandHandler[(string)command.GetCustomAttribute<CommandAttribute>().Tag] += (e)
                    => InitializeCommand(command, e);
            }

            InitializeOneTimeCommand(commandNamespace);
        }
    }
}
