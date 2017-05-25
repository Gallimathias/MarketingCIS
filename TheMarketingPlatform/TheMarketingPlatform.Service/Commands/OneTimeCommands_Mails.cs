using CommandManagementSystem.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.JSON;
using TheMarketingPlatform.Core.Network;

namespace TheMarketingPlatform.Service.Commands
{
    public static partial class OneTimeCommands
    {
        [OneTimeCommand("gettickets")]
        public static object Gettickets(KeyValuePair<TcpConnection, byte[]> args)
        {
            var list = new List<byte[]>();

            foreach (var ticket in SettingsHandler.DatabaseController.Tickets)
            {
                list.Add(Ticket.Serialize(ticket));
            }

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(list.Count);

                    foreach (var byteArray in list)
                    {
                        writer.Write(byteArray.Length);
                        writer.Write(byteArray);
                    }
                }

                args.Key.Send(new NetworkMessage("tickets", stream.ToArray()));
            }


            return args;
        }
    }
}
