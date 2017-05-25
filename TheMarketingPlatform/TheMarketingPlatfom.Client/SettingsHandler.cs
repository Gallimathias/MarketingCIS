using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheMarketingPlatform.Core;
using TheMarketingPlatform.Core.JSON;
using TheMarketingPlatform.Core.Network;
using System.Collections;
using System.IO;
using System.Collections.ObjectModel;

namespace TheMarketingPlatform.Client
{
    public class SettingsHandler : ConfigManagement
    {
        public TcpClient Client
        {
            get => client;
            set
            {
                client = value;
                client.OnConnect += (s) => ClientIsReady?.Invoke(this, client);
                StartTimer();
            }
        }
        
        private TcpClient client;
        private Timer timer;

        public event EventHandler<TcpClient> ClientIsReady;
        public event EventHandler<Notification> OnNotify;

        public SettingsHandler()
        {
            var path = Registry.GetValue(
                $@"HKEY_CURRENT_USER\SOFTWARE\Gallimathias\TheMarketingPlatform\Main", "Config", null)
                as string;

            if (string.IsNullOrEmpty(path))
                throw new Exception("Configuration file does not exist");

            var loadingResult = Load(path);

            if (!loadingResult.loaded)
                throw new Exception("Load file failed", loadingResult.exception);
        }

        public int GetMailAccountsCount()
        {
            Client?.Send(new NetworkMessage("getmailaccountscount", new byte[0]));
            var message = Client?.ReciveMessage();

            if (message.IsEmpty)
                return 0;

            return BitConverter.ToInt32(message.Payload, 0);
        }

        public ObservableCollection<MailAccount> GetMailAccounts()
        {
            Client?.Send(new NetworkMessage("getmailaccounts", new byte[0]));
            var message = Client?.ReciveMessage();

            if (message.IsEmpty)
                return null;

            var list = new ObservableCollection<MailAccount>();

            using (var stream = new MemoryStream(message.Payload))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var count = reader.ReadInt32();

                    for (int i = 0; i < count; i++)
                    {
                        list.Add(MailAccount.Deserialize(
                            reader.ReadBytes(reader.ReadInt32())));
                    }
                }
            }

            return list;
        }

        public void AddMailAccounts(IList newItems, IList oldItems)
        {
            var list = new List<byte[]>();

            foreach (var item in newItems)
            {
                if (oldItems.Contains(item))
                    continue;
                if (item is MailAccount mailAccount)
                    list.Add(MailAccount.Serialize(mailAccount));
            }

            Client.Send(new NetworkMessage("addmailaccounts", TcpConnection.GetDataFromList(list)));
        }

        public void RemoveMailAccounts(IList newItems, IList oldItems)
        {
            var list = new List<byte[]>();

            foreach (var item in oldItems)
            {
                if (newItems.Contains(item))
                    continue;
                if (item is MailAccount mailAccount)
                    list.Add(MailAccount.Serialize(mailAccount));
            }

            Client.Send(new NetworkMessage("removemailaccounts", TcpConnection.GetDataFromList(list)));
        }

        public ObservableCollection<Ticket> GetTickets()
        {
            Client?.Send(new NetworkMessage("gettickets", new byte[0]));
            var message = Client?.ReciveMessage();

            if (message.IsEmpty)
                return null;

            var list = new ObservableCollection<Ticket>();

            using (var stream = new MemoryStream(message.Payload))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var count = reader.ReadInt32();

                    for (int i = 0; i < count; i++)
                    {
                        list.Add(Ticket.Deserialize(
                            reader.ReadBytes(reader.ReadInt32())));
                    }
                }
            }

            return list;
        }

        public void Notifycate(object sender, Notification notification) => OnNotify?.Invoke(sender, notification);

        public Config GetServerSettings()
        {
            Client?.Send(new NetworkMessage("getserverconfig", new byte[0]));
            var message = Client?.ReciveMessage();

            if (message.IsEmpty)
                return null;

            return JsonConvert.DeserializeObject<Config>(Encoding.UTF8.GetString(message.Payload));
        }

        public void SendConfigToServer(Config serverConfig)
        {
            var payload = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(serverConfig));
            Client?.Send(new NetworkMessage("setserverconfig", payload));
            Client?.ReciveMessage();
        }

        private void CallNewMessage(object state)
        {
            client.Send(new NetworkMessage("newmessage", new byte[0]));
            var message = client.ReciveMessage();

            if (message.IsEmpty)
                return;

            if (message.Tag == "newmessage")
                Notifycate(this, new Notification()
                {
                    Timeout = 6000,
                    TipIcon = 0,
                    TipText = "Theres a new Message",
                    TipTitle = "New Ticket"
                });
        }

        private void StartTimer()
        {
            timer = new Timer(CallNewMessage, null, 0, 6000);
        }

    }
}
