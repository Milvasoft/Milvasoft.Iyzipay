using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class CardList : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string CardUserKey { get; set; }
        public List<Card> CardDetails { get; set; }

        public CardList(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<CardList> RetrieveAsync(RetrieveCardListRequest request)
        {
            return await _restHttpClient.PostAsync<CardList>("/cardstorage/cards", request).ConfigureAwait(false);
        }
    }
}
