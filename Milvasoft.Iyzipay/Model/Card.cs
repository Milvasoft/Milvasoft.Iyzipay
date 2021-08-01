using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class Card : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string ExternalId { get; set; }
        public string Email { get; set; }
        public string CardUserKey { get; set; }
        public string CardToken { get; set; }
        public string CardAlias { get; set; }
        public string BinNumber { get; set; }
        public string CardType { get; set; }
        public string CardAssociation { get; set; }
        public string CardFamily { get; set; }
        public long? CardBankCode { get; set; }
        public string CardBankName { get; set; }

        public Card(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<Card> CreateAsync(CreateCardRequest request) => await _restHttpClient.PostAsync<Card>("/cardstorage/card", request).ConfigureAwait(false);

        public async Task<Card> DeleteAsync(DeleteCardRequest request) => await _restHttpClient.DeleteAsync<Card>("/cardstorage/card", request).ConfigureAwait(false);
    }
}
