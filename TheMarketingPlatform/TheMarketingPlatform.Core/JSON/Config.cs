using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core.JSON
{
    internal class Config
    {
        public string Version { get; set; }
        public string ProductName { get; set; }
        public string LuisAppId { get; internal set; }
        public string LuisAppKey { get; internal set; }
    }
}
