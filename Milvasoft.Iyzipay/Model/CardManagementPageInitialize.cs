using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model
{
    public class CardManagementPageInitialize : IyzipayResource
    {
        private readonly IRestHttpClient _restHttpClient;

        public string ExternalId { get; set; }
        public string Token { get; set; }
        public string CardPageUrl { get; set; }

        public CardManagementPageInitialize(IRestHttpClient restHttpClient)
        {
            _restHttpClient = restHttpClient;
        }

        public async Task<CardManagementPageInitialize> CreateAsync(CreateCardManagementPageInitializeRequest request)
        {
            return await _restHttpClient.PostAsync<CardManagementPageInitialize>("/v1/card-management/pages", request).ConfigureAwait(false);
        }
    }

}