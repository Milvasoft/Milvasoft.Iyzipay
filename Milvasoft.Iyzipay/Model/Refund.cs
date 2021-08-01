using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class Refund : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string PaymentId { get; set; }
        public string PaymentTransactionId { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string ConnectorName { get; set; }
        public string AuthCode { get; set; }
        public string HostReference { get; set; }



        public Refund(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<Refund> CreateAsync(CreateRefundRequest request)
        {
            return await _restHttpClient.PostAsync<Refund>("/payment/refund", request).ConfigureAwait(false);
        }

        public async Task<Refund> CreateAmountBasedRefundRequestAsync(CreateAmountBasedRefundRequest request)
        {
            return await _restHttpClient.PostAsync<Refund>("/v2/payment/refund", request).ConfigureAwait(false);
        }

    }
}
