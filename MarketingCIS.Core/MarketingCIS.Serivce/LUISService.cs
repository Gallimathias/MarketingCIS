using MarketingCIS.Serivce.CIS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace MarketingCIS.Serivce
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
            luisClient = new LUISClient("","");
            
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
