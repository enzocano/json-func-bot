using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Schema;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.Bot.Builder.Integration.Functions
{
    internal static class HttpHelper
    {
        private static readonly JsonSerializerSettings BotMessageSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            ContractResolver = new ReadOnlyJsonContractResolver(),
            Converters = new List<JsonConverter> { new Iso8601TimeSpanConverter() }
        };

        public static readonly JsonSerializer BotMessageSerializer = JsonSerializer.Create(BotMessageSerializerSettings);

        public static Activity ReadRequest(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var activity = default(Activity);

            using (var bodyReader = new JsonTextReader(new StreamReader(request.Body, Encoding.UTF8)))
            {
                activity = BotMessageSerializer.Deserialize<Activity>(bodyReader);
            }

            return activity;
        }

        public static IActionResult GenerateResponse(InvokeResponse invokeResponse)
        {
            if (invokeResponse == null)
            {
                return new OkResult();
            }
            else
            {
                return new JsonResult(invokeResponse.Body, BotMessageSerializerSettings)
                {
                    ContentType = "application/json",
                    StatusCode = invokeResponse.Status
                };
            }
        }
    }
}
