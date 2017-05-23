using Newtonsoft.Json;
using System;

namespace TheMarketingPlatform.Core.CognitiveServices
{
    public abstract class CognitiveServiceClient<TResponse> 
    {
        public string AppID { get; protected set; }
        public string BaseURL => $"{baseUrlString}/{serviceName}/{serviceVersion}/apps";
        protected string key;
        protected string serviceName;
        protected string serviceVersion;
        protected string baseUrlString;
        protected bool staging;
        protected bool verbose;
        protected DateTimeOffset dateTimeOffset;

        public CognitiveServiceClient(string appID, string key)
        {
            AppID = appID;
            this.key = key;
            baseUrlString = "westus.api.cognitive.microsoft.com";
            serviceVersion = "v2.0";
            serviceName = "";
            staging = true;
            verbose = true;
            dateTimeOffset = new DateTimeOffset();
        }

        protected TResponse GetResult(string result) => JsonConvert.DeserializeObject<TResponse>(result);


    }
}