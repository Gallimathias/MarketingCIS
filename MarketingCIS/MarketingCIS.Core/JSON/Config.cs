using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingCIS.Core.JSON
{
    public class Config
    {
        public string version { get; set; }
        public string productName { get; set; }
        public string luisAppId { get; internal set; }
        public string luisAppKey { get; internal set; }
        public string mailHost { get; internal set; }
        public int mailPort { get; internal set; }
        public bool mailSSL { get; internal set; }
    }
}
