﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Milvasoft.Iyzipay.Utils.Concrete
{
    public class JsonBuilder
    {
        public static string SerializeToJsonString(BaseRequest request)
        {
            return JsonConvert.SerializeObject(request, new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static string SerializeToJsonString(BaseRequestV2 request)
        {
            return JsonConvert.SerializeObject(request, new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static string SerializeObjectToPrettyJson(BaseRequestV2 value)
        {
            StringBuilder sb = new(256);
            StringWriter sw = new(sb, CultureInfo.InvariantCulture);

            var jsonSerializer = JsonSerializer.CreateDefault();
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
            jsonSerializer.DefaultValueHandling = DefaultValueHandling.Ignore;
            jsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerializer.Formatting = Formatting.Indented;

            using (JsonTextWriter jsonWriter = new(sw))
            {
                jsonWriter.Formatting = Formatting.Indented;
                jsonWriter.IndentChar = '\t';
                jsonWriter.Indentation = 1;

                jsonSerializer.Serialize(jsonWriter, value, typeof(BaseRequestV2));
            }

            string json = sw.ToString();
            return json.Replace("\r", "");
        }

        public static StringContent ToJsonString(BaseRequest request)
        {
            return new StringContent(SerializeToJsonString(request), Encoding.Unicode, "application/json");
        }

        public static StringContent ToJsonString(BaseRequestV2 request)
        {
            return new StringContent(SerializeObjectToPrettyJson(request), Encoding.UTF8, "application/json");
        }
    }
}
