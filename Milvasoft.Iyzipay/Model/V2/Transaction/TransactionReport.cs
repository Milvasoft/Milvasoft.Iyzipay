using Milvasoft.Iyzipay.Request.V2;
using Milvasoft.Iyzipay.Utils.Abstract;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model.V2.Transaction
{
    public class TransactionReport : TransactionReportResource
    {
        private readonly IRestHttpClientV2 _restHttpClientV2;

        public TransactionReport(IRestHttpClientV2 restHttpClientV2)
        {
            _restHttpClientV2 = restHttpClientV2;
        }


        public async Task<TransactionReport> Retrieve(RetrieveTransactionReportRequest request)
        {
            string url = "/v2/reporting/payment/transactions?transactionDate="
                            + request.TransactionDate
                            + "&page="
                            + request.Page;
            return await _restHttpClientV2.GetAsync<TransactionReport>(url, request, false).ConfigureAwait(false);
        }
    }
}
