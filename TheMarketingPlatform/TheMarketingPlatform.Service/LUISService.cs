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
    internal partial class LUISService : ServiceBase
    {
        public SettingsHandler SettingsHandler { get; private set; }

        private LUISClient luisClient;
        private ConcurrentQueue<MimeMessage> messageQueue;

        public LUISService(SettingsHandler settingsHandler)
        {
            InitializeComponent();
            SettingsHandler = settingsHandler;
        }
        
        protected override void OnStart(string[] args)
        {
            
        }

        protected override void OnStop()
        {
            
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
            foreach (var message in mimeMessages)
                messageQueue.Enqueue(message);
        }
    }
}
