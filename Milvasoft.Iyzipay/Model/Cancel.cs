using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class Cancel : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string PaymentId { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string ConnectorName { get; set; }
        public string AuthCode { get; set; }
        public string HostReference { get; set; }

        public Cancel(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<Cancel> CreateAsync(CreateCancelRequest request)
        {
            return await _restHttpClient.PostAsync<Cancel>("/payment/cancel", request).ConfigureAwait(false);
        }
    }
}
