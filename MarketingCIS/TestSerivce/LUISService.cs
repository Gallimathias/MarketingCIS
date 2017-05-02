﻿using MarketingCIS.Core.CognitiveServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MarketingCIS.Core;

namespace TestSerivce
{
    class LUISService
    {
        private Task task;
        private CancellationTokenSource cancelTokenSource;
        private LUISClient luisClient;

        public LUISService()
        {
            //InitializeComponent();
            cancelTokenSource = new CancellationTokenSource();
            luisClient = new LUISClient(ConfigMgt.LuisAppId, ConfigMgt.LuisAppKey);
        }

        public  void OnStart(string[] args)
        {
            task = Task.Run(() => Process(), cancelTokenSource.Token);
        }

        public  void OnStop()
        {
            cancelTokenSource.Cancel(false);
        }

        private void Process()
        {

        }
    }
}

