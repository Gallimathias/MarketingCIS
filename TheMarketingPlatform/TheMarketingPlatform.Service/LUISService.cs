using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using MimeKit;
using TheMarketingPlatform.Core;
using TheMarketingPlatform.Core.CognitiveServices;
using TheMarketingPlatform.CognitiveServices;
using System.Collections.Concurrent;

namespace TheMarketingPlatform.Service
{
    internal partial class LUISService
    {
        public SettingsHandler SettingsHandler { get; private set; }

        private LUISClient luisClient;
        private ConcurrentQueue<MimeMessage> messageQueue;

        public LUISService(SettingsHandler settingsHandler)
        {
            SettingsHandler = settingsHandler;
            messageQueue = new ConcurrentQueue<MimeMessage>();
            luisClient = new LUISClient(
                (string)settingsHandler["LUISAppId"],
                (string)settingsHandler["LUISAppKey"]);
        }
        
        private void StartProcess()
        {
            Task.Run(() =>
            {
                while (messageQueue.Count > 0)
                    Process();

            });

        }

        private void Process()
        {
            if (!messageQueue.TryDequeue(out MimeMessage mimeMessage))
                return;

            var response = luisClient.Reply(mimeMessage.TextBody);
            var messageid = SettingsHandler.DatabaseController.Insert(mimeMessage);

            SettingsHandler.DatabaseController.Insert(response, messageid);
        }

        internal void HandleMessages(MimeMessage[] mimeMessages)
        {
            SettingsHandler.NewMessage = true;

            foreach (var message in mimeMessages)
                messageQueue.Enqueue(message);

            StartProcess();
        }
    }
}
