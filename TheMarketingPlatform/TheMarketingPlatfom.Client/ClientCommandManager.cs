using CommandManagementSystem;
using CommandManagementSystem.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Client;
using TheMarketingPlatform.Client.Commands;
using TheMarketingPlatform.Core.Network;

namespace TheMarketingPlatform.Client
{
    [CommandManager("ClientCommandManager", "TheMarketingPlatform.Client.Commands")]
    public class ClientCommandManager : CommandManager<string, KeyValuePair<TcpClient, byte[]>, object>
    {
        SettingsHandler settingsHandler;
        TcpClient client;

        public ClientCommandManager(SettingsHandler settingsHandler) : base()
        {
            this.settingsHandler = settingsHandler;
            
            settingsHandler.CommandManager = this;
            OneTimeCommands.SettingsHandler = settingsHandler;

            settingsHandler.ClientIsReady += (o, t) =>
            { 
                Task.Run(() =>
                {
                    while (settingsHandler.Client == null || !settingsHandler.Client.Connected) ;
                    client = settingsHandler.Client;
                    client.OnMessageRecived += (s, e) =>
                        DispatchAsync(e.Tag, new KeyValuePair<TcpClient, byte[]>(client, e.Payload));
                    client.BeginRecive();
                });

            };
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
                    commandHandler[(string)member.GetCustomAttribute<OneTimeCommandAttribute>().Tag] += (Func<KeyValuePair<TcpClient, byte[]>, object>)(
                        (MethodInfo)member).CreateDelegate(typeof(Func<KeyValuePair<TcpClient, byte[]>, object>));
                }

            }
        }
    }
}
