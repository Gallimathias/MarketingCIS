using CommandManagementSystem;
using CommandManagementSystem.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.Network;
using TheMarketingPlatform.Service.Commands;

namespace TheMarketingPlatform.Service
{
    [CommandManager("ServiceCommandManager", "TheMarketingPlatform.Service.Commands")]
    internal class ServiceCommandManager : CommandManager<string, KeyValuePair<TcpConnection, byte[]>, object>
    {
        SettingsHandler settingsHandler;

        public ServiceCommandManager(SettingsHandler settingsHandler) : base()
        {
            this.settingsHandler = settingsHandler;
        }

        public override void Initialize()
        {
            var commandNamespace = GetType().GetCustomAttribute<CommandManagerAttribute>().CommandNamespaces;

            var commands = Assembly.GetExecutingAssembly().GetTypes().Where(
                t => t.GetCustomAttribute<CommandAttribute>() != null && commandNamespace.Contains(t.Namespace)).ToList();

            foreach (var command in commands)
            {
                commandHandler[(string)command.GetCustomAttribute<CommandAttribute>().Tag] += (e)
                    => InitializeCommand(command, e, settingsHandler);
            }

            InitializeOneTimeCommand(commandNamespace);
        }

        public new void InitializeOneTimeCommand(string[] namespaces)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(
                t => namespaces.Contains(t.Namespace)).ToArray();

            for (int i = 0; i < types.Length; i++)
            {
                var members = types[i].GetMembers(BindingFlags.Static | BindingFlags.Public).Where(
                    m => m.GetCustomAttribute<OneTimeCommandAttribute>() != null);
                foreach (var member in members)
                {
                    commandHandler[(string)member.GetCustomAttribute<OneTimeCommandAttribute>().Tag] += (Func<KeyValuePair<TcpConnection, byte[]>, object>)(
                        (MethodInfo)member).CreateDelegate(typeof(Func<KeyValuePair<TcpConnection, byte[]>, object>));
                }

            }
        }
    }
}
