using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingCIS.Serivce.CognitiveServices
{
    class LUISClient : CognitiveServiceClient
    {
        public LUISClient(string appID, string key) : base(appID, key)
        {
        }
    }
}
