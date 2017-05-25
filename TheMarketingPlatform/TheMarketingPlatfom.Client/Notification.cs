using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Client
{
    public class Notification
    {
        public int TipIcon { get; set; }
        public string TipText { get; set; }
        public string TipTitle { get; set; }
        public int Timeout { get; set; }
    }
}
