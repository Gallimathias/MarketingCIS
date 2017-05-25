using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheMarketingPlatform.Core.CognitiveServices;
using TheMarketingPlatform.Core.JSON;

namespace TheMarketingPlatform.CognitiveServices
{
    /// <summary>
    /// A LUIS implementation of Cognitive services
    /// </summary>
    public class LUISClient : CognitiveServiceClient<Response>
    {

        public LUISClient(string appID, string key) : base(appID, key)
        {
            serviceName = "luis";
        }

        public Response Reply(string message)
        {
            var request = WebRequest.Create(new Uri(
                $"https://{BaseURL}/{AppID}?subscription-key={key}&staging={staging}&verbose={verbose}&timezoneOffset={dateTimeOffset.ToString("H.m")}&q={message}"));

            using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                return GetResult(reader.ReadToEnd());
            }
        }
    }
}
