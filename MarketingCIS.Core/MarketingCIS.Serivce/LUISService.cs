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
        private CancellationToken cancelToken;

        public LUISService()
        {
            InitializeComponent();
            cancelToken = new CancellationToken();
            
        }

        protected override void OnStart(string[] args)
        {
            task = Task.Run(() => Process(), cancelToken);
        }

        protected override void OnStop()
        {
            
        }
        
        private void Process()
        {

        }
    }
}
