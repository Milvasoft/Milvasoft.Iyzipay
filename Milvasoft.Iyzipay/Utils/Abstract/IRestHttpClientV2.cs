using Milvasoft.Iyzipay.Utils.Concrete;
using System;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Utils.Abstract
{
    public interface IRestHttpClientV2 : IDisposable
    {
        public IOptions Options { get; }

        Task<T> GetAsync<T>(string url, BaseRequestV2 request, bool headersWithRequestBody) where T : IyzipayResourceV2;

        Task<T> PostAsync<T>(string url, BaseRequestV2 request, bool headersWithRequestBody) where T : IyzipayResourceV2;

        Task<T> PutAsync<T>(string url, BaseRequestV2 request, bool headersWithRequestBody) where T : IyzipayResourceV2;

        Task<T> DeleteAsync<T>(string url, BaseRequestV2 request, bool headersWithRequestBody) where T : IyzipayResourceV2;
    }
}
