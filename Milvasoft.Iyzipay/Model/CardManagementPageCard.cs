using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class CardManagementPageCard : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        private string ExternalId { get; set; }
        private string CardUserKey { get; set; }
        private List<Card> CardDetails { get; set; }


        public CardManagementPageCard(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }


        public async Task<CardManagementPageCard> RetrieveAsync(RetrieveCardManagementPageCardRequest request)
        {
            return await _restHttpClient.GetAsync<CardManagementPageCard>(PrepareRetrieveCardManagementPageCardRequest(request), request).ConfigureAwait(false);
        }

        private string PrepareRetrieveCardManagementPageCardRequest(RetrieveCardManagementPageCardRequest request)
        {
            return $"/v1/card-management/pages/{request.PageToken}/cards?locale={request.Locale}&conversationId={request.ConversationId}";
        }
    }
}