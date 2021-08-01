using Milvasoft.Iyzipay.Utils.Concrete;
using System;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Utils.Abstract
{
    public interface IRestHttpClient : IDisposable
    {
        public IOptions Options { get; }

        Task<T> GetAsync<T>(string url);

        Task<T> GetAsync<T>(string url, BaseRequest request);

        Task<T> PostAsync<T>(string url, BaseRequest request);

        Task<T> DeleteAsync<T>(string url, BaseRequest request);

        Task<T> PutAsync<T>(string url, BaseRequest request);
    }
}
