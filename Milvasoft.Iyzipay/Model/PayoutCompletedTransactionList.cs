using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class PayoutCompletedTransactionList : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public List<PayoutCompletedTransaction> PayoutCompletedTransactions { get; set; }

        public PayoutCompletedTransactionList(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<PayoutCompletedTransactionList> RetrieveAsync(RetrieveTransactionsRequest request)
        {
            return await _restHttpClient.PostAsync<PayoutCompletedTransactionList>("/reporting/settlement/payoutcompleted", request).ConfigureAwait(false);
        }
    }
}
