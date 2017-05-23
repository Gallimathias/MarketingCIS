﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingPlatform.Core.CognitiveServices
{
    public class LUISClient : CognitiveServiceClient
    {
        public LUISClient(string appID, string key) : base(appID, key)
        {
        }

        public void Reply(string message)
        {
            var request = WebRequest.Create(
                $"{BaseURL}/{AppID}?subscription-key={key}&staging={staging}&verbose={verbose}&timezoneOffset={dateTimeOffset.ToString("H.m")}&q={message}");

            using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                reader.ReadToEnd();
            }
        }
    }
}