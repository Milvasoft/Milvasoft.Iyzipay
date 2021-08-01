using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class BouncedBankTransferList : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        [JsonProperty(PropertyName = "bouncedRows")]
        public List<BankTransfer> BankTransfers { get; set; }

        public BouncedBankTransferList(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<BouncedBankTransferList> RetrieveAsync(RetrieveTransactionsRequest request)
        {
            return await _restHttpClient.PostAsync<BouncedBankTransferList>("/reporting/settlement/bounced", request).ConfigureAwait(false);
        }
    }
}
