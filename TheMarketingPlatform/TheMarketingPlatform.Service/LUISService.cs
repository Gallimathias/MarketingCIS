using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.CognitiveServices;

namespace TheMarketingPlatform.Service
{
    public partial class LUISService : ServiceBase
    {
        private Task task;
        private CancellationTokenSource cancelTokenSource;
        private LUISClient luisClient;

        public LUISService()
        {
            InitializeComponent();
            cancelTokenSource = new CancellationTokenSource();
            luisClient = new LUISClient(ConfigMgt.LuisAppId, ConfigMgt.LuisAppKey);
        }

        protected override void OnStart(string[] args)
        {
            task = Task.Run(() => Process(), cancelTokenSource.Token);
        }

        protected override void OnStop()
        {
            cancelTokenSource.Cancel(false);
        }

        private void Process()
        {

        }
    }
}
