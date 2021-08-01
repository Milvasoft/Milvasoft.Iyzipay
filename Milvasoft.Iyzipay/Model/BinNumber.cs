using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class BinNumber : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        [JsonProperty(PropertyName = "binNumber")]
        public string Bin { get; set; }
        public string CardType { get; set; }
        public string CardAssociation { get; set; }
        public string CardFamily { get; set; }
        public string BankName { get; set; }
        public long BankCode { get; set; }
        public int Commercial { get; set; }

        public BinNumber(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<BinNumber> RetrieveAsync(RetrieveBinNumberRequest request)
        {
            return await _restHttpClient.PostAsync<BinNumber>("/payment/bin/check", request).ConfigureAwait(false);
        }
    }
}
