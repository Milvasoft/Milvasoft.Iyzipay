using Milvasoft.Iyzipay.Request.V2.Subscription;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model.V2.Subscription
{
    public class Plan : IyzipayResourceV2
    {
        private readonly IRestHttpClientV2 _restHttpClientV2;

        public Plan(IRestHttpClientV2 restHttpClientV2)
        {
            _restHttpClientV2 = restHttpClientV2;
        }

        public async Task<ResponseData<PlanResource>> CreateAsync(CreatePlanRequest request)
        {
            string uri = $"/v2/subscription/products/{request.ProductReferenceCode}/pricing-plans";
            return await _restHttpClientV2.PostAsync<ResponseData<PlanResource>>(uri, request, true).ConfigureAwait(false);
        }

        public async Task<ResponseData<PlanResource>> UpdateAsync(UpdatePlanRequest request)
        {
            string uri = $"/v2/subscription/pricing-plans/{request.PricingPlanReferenceCode}";
            return await _restHttpClientV2.PostAsync<ResponseData<PlanResource>>(uri, request, true).ConfigureAwait(false);
        }

        public async Task<IyzipayResourceV2> DeleteAsync(DeletePlanRequest request)
        {
            string uri = $"/v2/subscription/pricing-plans/{request.PricingPlanReferenceCode}";
            return await _restHttpClientV2.DeleteAsync<IyzipayResourceV2>(uri, request, true).ConfigureAwait(false);
        }

        public async Task<ResponseData<PlanResource>> RetrieveAsync(RetrievePlanRequest request)
        {
            string uri = $"/v2/subscription/pricing-plans/{request.PricingPlanReferenceCode}";
            return await _restHttpClientV2.GetAsync<ResponseData<PlanResource>>(uri, request, false).ConfigureAwait(false);
        }

        public async Task<ResponsePagingData<PlanResource>> RetrieveAllAsync(RetrieveAllPlanRequest request)
        {
            string uri = $"/v2/subscription/products/{request.ProductReferenceCode}/pricing-plans{GetQueryParams(request)}";
            return await _restHttpClientV2.GetAsync<ResponsePagingData<PlanResource>>(uri, request, false).ConfigureAwait(false);
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