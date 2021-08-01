using Milvasoft.Iyzipay.Utils.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Utils.Concrete
{
    public class RestHttpClient : IRestHttpClient
    {
        private static readonly string AUTHORIZATION = "Authorization";
        private static readonly string RANDOM_HEADER_NAME = "x-iyzi-rnd";
        private static readonly string CLIENT_VERSION_HEADER_NAME = "x-iyzi-client-version";
        private static readonly string IYZIWS_HEADER_NAME = "IYZWS ";
        private static readonly string COLON = ":";
        private readonly HttpClient _httpClient;
        private bool _disposedValue;

        public IOptions Options { get; private set; }

        public RestHttpClient(HttpClient httpClient, IOptions options)
        {
#if !NETSTANDARD
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#endif

            _httpClient = httpClient;
            Options = options;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            using HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(Options.BaseUrl + url).ConfigureAwait(false);

            var stringResponse = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(stringResponse);
        }

        public async Task<T> GetAsync<T>(string url, BaseRequest request = null)
            => await GetResponseAsync<T>(HttpMethod.Get, url, request).ConfigureAwait(false);

        public async Task<T> PostAsync<T>(string url, BaseRequest request)
            => await GetResponseAsync<T>(HttpMethod.Post, url, request).ConfigureAwait(false);

        public async Task<T> DeleteAsync<T>(string url, BaseRequest request)
            => await GetResponseAsync<T>(HttpMethod.Delete, url, request).ConfigureAwait(false);

        public async Task<T> PutAsync<T>(string url, BaseRequest request)
            => await GetResponseAsync<T>(HttpMethod.Put, url, request).ConfigureAwait(false);

        private async Task<T> GetResponseAsync<T>(HttpMethod httpMethod, string url, BaseRequest request = null)
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

            var headers = GetHttpHeaders(request);

            foreach (var header in headers)
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            using HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);

            var stringResponse = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(stringResponse);
        }

        #region Private Methods

        private Dictionary<string, string> GetHttpHeaders(BaseRequest request)
        {
            string randomString = DateTime.Now.ToString("ddMMyyyyhhmmssffff");

            Dictionary<string, string> headers = new();

            headers.Add("Accept", "application/json");
            headers.Add(RANDOM_HEADER_NAME, randomString);
            headers.Add(CLIENT_VERSION_HEADER_NAME, IyzipayConstants.CLIENT_VERSION);
            headers.Add(AUTHORIZATION, PrepareAuthorizationString(request, randomString));

            return headers;
        }

        private string PrepareAuthorizationString(BaseRequest request, string randomString)
        {
            string hash = HashGenerator.GenerateHash(Options.ApiKey, Options.SecretKey, randomString, request);
            return IYZIWS_HEADER_NAME + Options.ApiKey + COLON + hash;
        }

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

        ~RestHttpClient()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
