using Newtonsoft.Json;

namespace TheMarketingPlatform.Core.JSON
{
    public class Intent
    {
        [JsonProperty("intent")]
        public string IntentName { get; set; }
        public float Score { get; set; }
    }

}
