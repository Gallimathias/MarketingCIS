using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using MimeKit;
using TheMarketingPlatform.Core;
using TheMarketingPlatform.Core.CognitiveServices;

namespace TheMarketingPlatform.Service
{
    internal partial class LUISService : ServiceBase
    {
        private LUISClient luisClient;

        public LUISService()
        {
            InitializeComponent();            
        }
        
        protected override void OnStart(string[] args)
        {
            
        }

        protected override void OnStop()
        {
            
        }

        private void Process()
        {

        }

        internal void HandleMessages(MimeMessage[] m)
        {
            throw new NotImplementedException();
        }
    }
}
