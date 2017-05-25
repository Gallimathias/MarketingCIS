using Newtonsoft.Json;
using System;

namespace TheMarketingPlatform.Core.CognitiveServices
{
    /// <summary>
    /// Basic class for CognitiveSerivces
    /// </summary>
    /// <typeparam name="TResponse">Return type of the API response of the service</typeparam>
    public abstract class CognitiveServiceClient<TResponse> 
    {
        /// <summary>
        /// The AppID fro the API call
        /// </summary>
        public string AppID { get; protected set; }
        /// <summary>
        /// The base microsoft api call
        /// </summary>
        public string BaseURL => $"{baseUrlString}/{serviceName}/{serviceVersion}/apps";
        /// <summary>
        /// The API key for the api call
        /// </summary>
        protected string key;
        /// <summary>
        /// API service name not the Service ID
        /// </summary>
        protected string serviceName;
        /// <summary>
        /// The API version, current version is 2.0
        /// </summary>
        protected string serviceVersion;
        /// <summary>
        /// Base url oft the BaseUrl
        /// </summary>
        protected string baseUrlString;
        /// <summary>
        /// Set true if API use staging key
        /// </summary>
        protected bool staging;
        /// <summary>
        /// If true then you get the detailed result
        /// </summary>
        protected bool verbose;
        /// <summary>
        /// Current DateTime
        /// </summary>
        protected DateTimeOffset dateTimeOffset;

        /// <summary>
        /// Basic class for CognitiveSerivces
        /// </summary>
        /// <param name="appID">The ID of yout App API</param>
        /// <param name="key">The API key of your App</param>
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

        /// <summary>
        /// Gets the result with JsonConvert from api
        /// </summary>
        /// <param name="result">The current result</param>
        /// <returns>The Deserialized result</returns>
        protected TResponse GetResult(string result) => JsonConvert.DeserializeObject<TResponse>(result);


    }
}