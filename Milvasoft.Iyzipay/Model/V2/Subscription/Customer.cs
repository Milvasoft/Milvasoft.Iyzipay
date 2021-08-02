using Milvasoft.Iyzipay.Request.V2.Subscription;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model.V2.Subscription
{
    public class Customer : IyzipayResourceV2
    {
        private readonly IRestHttpClientV2 _restHttpClientV2;

        public Customer(IRestHttpClientV2 restHttpClientV2)
        {
            _restHttpClientV2 = restHttpClientV2;
        }

        public async Task<ResponseData<CustomerResource>> CreateAsync(CreateCustomerRequest request)
        {
            string uri = $"/v2/subscription/customers";
            return await _restHttpClientV2.PostAsync<ResponseData<CustomerResource>>(uri, request, true).ConfigureAwait(false);
        }

        public async Task<ResponseData<CustomerResource>> UpdateAsync(UpdateCustomerRequest request)
        {
            string uri = $"/v2/subscription/customers/{request.CustomerReferenceCode}";
            return await _restHttpClientV2.PostAsync<ResponseData<CustomerResource>>(uri, request, true).ConfigureAwait(false);
        }

        public async Task<ResponseData<CustomerResource>> RetrieveAsync(RetrieveCustomerRequest request)
        {
            string uri = $"/v2/subscription/customers/{request.CustomerReferenceCode}";
            return await _restHttpClientV2.GetAsync<ResponseData<CustomerResource>>(uri, request, false).ConfigureAwait(false);
        }

        public async Task<ResponsePagingData<CustomerResource>> RetrieveAllAsync(PagingRequest request)
        {
            string uri = $"/v2/subscription/customers{GetQueryParams(request)}";
            return await _restHttpClientV2.GetAsync<ResponsePagingData<CustomerResource>>(uri, request, false).ConfigureAwait(false);
        }


        private static string GetQueryParams(BaseRequestV2 request)
        {
            if (request == null)
            {
                return "";
            }

            string queryParams = "?conversationId=" + request.ConversationId;

            if (!string.IsNullOrEmpty(request.Locale))
            {
                queryParams += "&locale=" + request.Locale;
            }

            if (request is not PagingRequest pagingRequest) return queryParams;

            if (pagingRequest.Page != null)
            {
                queryParams += "&page=" + pagingRequest.Page;
            }

            if (pagingRequest.Count != null)
            {
                queryParams += "&count=" + pagingRequest.Count;
            }
            return queryParams;
        }
    }
}