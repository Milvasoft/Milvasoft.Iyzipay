using Milvasoft.Iyzipay.Utils.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Utils.Concrete
{
    public class RestHttpClientV2 : IRestHttpClientV2
    {
        private static readonly string AUTHORIZATION = "Authorization";
        private static readonly string CONVERSATION_ID_HEADER_NAME = "x-conversation-id";
        private static readonly string CLIENT_VERSION_HEADER_NAME = "x-iyzi-client-version";
        private static readonly string IYZIWS_V2_HEADER_NAME = "IYZWSv2 ";
        private readonly HttpClient _httpClient;
        private bool _disposedValue;

        public IOptions Options { get; private set; }

        public RestHttpClientV2(HttpClient httpClient, IOptions options)
        {
#if !NETSTANDARD
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#endif

            _httpClient = httpClient;
            Options = options;
        }

        public async Task<T> GetAsync<T>(string url, BaseRequestV2 request, bool headersWithRequestBody) where T : IyzipayResourceV2
            => await GetResponseAsync<T>(HttpMethod.Get, url, headersWithRequestBody, request).ConfigureAwait(false);

        public async Task<T> PostAsync<T>(string url, BaseRequestV2 request, bool headersWithRequestBody) where T : IyzipayResourceV2
            => await GetResponseAsync<T>(HttpMethod.Post, url, headersWithRequestBody, request).ConfigureAwait(false);

        public async Task<T> PutAsync<T>(string url, BaseRequestV2 request, bool headersWithRequestBody) where T : IyzipayResourceV2
            => await GetResponseAsync<T>(HttpMethod.Put, url, headersWithRequestBody, request).ConfigureAwait(false);

        public async Task<T> DeleteAsync<T>(string url, BaseRequestV2 request, bool headersWithRequestBody) where T : IyzipayResourceV2
            => await GetResponseAsync<T>(HttpMethod.Delete, url, headersWithRequestBody, request).ConfigureAwait(false);

        private async Task<T> GetResponseAsync<T>(HttpMethod httpMethod, string url, bool headersWithRequestBody, BaseRequestV2 request = null) where T : IyzipayResourceV2
        {
            using HttpRequestMessage requestMessage = new()
            {
                Method = httpMethod,
                RequestUri = new Uri(Options.BaseUrl + url),
                Content = httpMethod != HttpMethod.Get
                                ? request != null
                                        ? JsonBuilder.ToJsonString(request)
                                        : null
                                : null
            };

            var headers = headersWithRequestBody ? GetHttpHeadersWithRequestBody(request, url) : GetHttpHeadersWithUrlParams(request, url);

            foreach (var header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            using HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);

            var stringResponse = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            var response = JsonConvert.DeserializeObject<T>(stringResponse);

            AppendWithHttpResponseHeaders(httpResponseMessage, ref response);

            return response;
        }



        #region Private Methods

        private static void AppendWithHttpResponseHeaders<T>(HttpResponseMessage httpResponseMessage, ref T response) where T : IyzipayResourceV2
        {
            HttpHeaders responseHeaders = httpResponseMessage.Headers;
            response.StatusCode = Convert.ToInt32(httpResponseMessage.StatusCode);

            if (responseHeaders.TryGetValues(CONVERSATION_ID_HEADER_NAME, out IEnumerable<string> values))
            {
                string conversationId = values.First();
                response.ConversationId = !string.IsNullOrWhiteSpace(conversationId) ? conversationId : null;
            }
        }

        private Dictionary<string, string> GetHttpHeadersWithRequestBody(BaseRequestV2 request, string url)
        {
            Dictionary<string, string> headers = GetCommonHttpHeaders(request);
            headers.Add(AUTHORIZATION, PrepareAuthorizationStringWithRequestBody(request, url));
            return headers;
        }

        private Dictionary<string, string> GetHttpHeadersWithUrlParams(BaseRequestV2 request, string url)
        {
            Dictionary<string, string> headers = GetCommonHttpHeaders(request);
            headers.Add(AUTHORIZATION, PrepareAuthorizationStringWithRequestBody(null, url));
            return headers;
        }

        private static Dictionary<string, string> GetCommonHttpHeaders(BaseRequestV2 request)
        {
            Dictionary<string, string> headers = new()
            {
                { "Accept", "application/json" },

                { CLIENT_VERSION_HEADER_NAME, IyzipayConstants.CLIENT_VERSION },

                { CONVERSATION_ID_HEADER_NAME, request.ConversationId }
            };

            return headers;
        }

        private string PrepareAuthorizationStringWithRequestBody(BaseRequestV2 request, string url)
        {
            string randomKey = GenerateRandomKey();

            string uriPath = FindUriPath(url);

            string payload = request != null ? uriPath + JsonBuilder.SerializeObjectToPrettyJson(request) : uriPath;

            string dataToEncrypt = randomKey + payload;

            string hash = HashGeneratorV2.GenerateHash(Options.ApiKey, Options.SecretKey, randomKey, dataToEncrypt);

            return IYZIWS_V2_HEADER_NAME + hash;
        }

        private string PrepareAuthorizationStringWithUrlParam(string url)
        {
            string randomKey = GenerateRandomKey();

            string uriPath = FindUriPath(url);

            string dataToEncrypt = randomKey + uriPath;

            string hash = HashGeneratorV2.GenerateHash(Options.ApiKey, Options.SecretKey, randomKey, dataToEncrypt);

            return IYZIWS_V2_HEADER_NAME + hash;
        }

        private static string GenerateRandomKey()
        {
            return DateTime.Now.ToString("ddMMyyyyhhmmssffff");
        }

        private static string FindUriPath(string url)
        {
            int startIndex = url.IndexOf("/v2");
            int endIndex = url.IndexOf("?");
            int length = endIndex == -1 ? url.Length - startIndex : endIndex - startIndex;
            return url.Substring(startIndex, length);
        }

        #endregion


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    //Dispose managed state (managed objects)
                    _httpClient?.Dispose();
                }

                _disposedValue = true;
            }
        }

        ~RestHttpClientV2()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
