using Milvasoft.Iyzipay.Request.V2;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model.V2.Transaction
{
    public class TransactionDetail : TransactionDetailResource
    {
        private readonly IRestHttpClientV2 _restHttpClientV2;

        public TransactionDetail(IRestHttpClientV2 restHttpClientV2)
        {
            _restHttpClientV2 = restHttpClientV2;
        }

        public async Task<TransactionDetail> Retrieve(RetrieveTransactionDetailRequest request)
        {
            string url = "/v2/reporting/payment/details?paymentConversationId=" + request.PaymentConversationId;
            return await _restHttpClientV2.GetAsync<TransactionDetail>(url, request, false).ConfigureAwait(false);
        }
    }
}
