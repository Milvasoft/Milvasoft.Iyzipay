using Milvasoft.Iyzipay.Request.V2.Subscription;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model.V2.Subscription
{
    public class Subscription : IyzipayResourceV2
    {
        private readonly IRestHttpClientV2 _restHttpClientV2;

        public Subscription(IRestHttpClientV2 restHttpClientV2)
        {
            _restHttpClientV2 = restHttpClientV2;
        }

        public async Task<CheckoutFormResource> InitializeCheckoutFormAsync(InitializeCheckoutFormRequest request)
        {
            string uri = $"/v2/subscription/checkoutform/initialize";
            return await _restHttpClientV2.PostAsync<CheckoutFormResource>(uri, request, true);
        }

        public async Task<UpdateCardFormResource> UpdateCardAsync(UpdateCardRequest request)
        {
            string uri = $"/v2/subscription/card-update/checkoutform/initialize";
            return await _restHttpClientV2.PostAsync<UpdateCardFormResource>(uri, request, true);
        }

        public async Task<ResponseData<SubscriptionCreatedResource>> RetrieveCheckoutFormResult(RetrieveCheckoutFormResultRequest request)
        {
            string uri = $"/v2/subscription/checkoutform/{request.Token}";
            return await _restHttpClientV2.GetAsync<ResponseData<SubscriptionCreatedResource>>(uri, request, false);
        }

        public async Task<ResponseData<SubscriptionCreatedResource>> InitializeAsync(SubscriptionInitializeRequest request)
        {
            string uri = $"/v2/subscription/initialize";
            return await _restHttpClientV2.PostAsync<ResponseData<SubscriptionCreatedResource>>(uri, request, true);
        }

        public async Task<IyzipayResourceV2> ActivateAsync(ActivateSubscriptionRequest request)
        {
            string uri = $"/v2/subscription/subscriptions/{request.SubscriptionReferenceCode}/activate";
            return await _restHttpClientV2.PostAsync<IyzipayResourceV2>(uri, request, true);
        }

        public async Task<IyzipayResourceV2> RetryAsync(RetrySubscriptionRequest request)
        {
            string uri = $"/v2/subscription/operation/retry";
            return await _restHttpClientV2.PostAsync<IyzipayResourceV2>(uri, request, true);
        }

        public async Task<IyzipayResourceV2> UpgradeAsync(UpgradeSubscriptionRequest request)
        {
            string uri = $"/v2/subscription/subscriptions/{request.SubscriptionReferenceCode}/upgrade";
            return await _restHttpClientV2.PostAsync<IyzipayResourceV2>(uri, request, true);
        }

        public async Task<IyzipayResourceV2> CancelAsync(CancelSubscriptionRequest request)
        {
            string uri = $"/v2/subscription/subscriptions/{request.SubscriptionReferenceCode}/cancel";
            return await _restHttpClientV2.PostAsync<IyzipayResourceV2>(uri, request, true);
        }

        public async Task<ResponseData<SubscriptionResource>> RetrieveAsync(RetrieveSubscriptionRequest request)
        {
            string uri = $"/v2/subscription/subscriptions/{request.SubscriptionReferenceCode}";
            return await _restHttpClientV2.GetAsync<ResponseData<SubscriptionResource>>(uri, request, false);
        }

        public async Task<ResponsePagingData<SubscriptionResource>> SearchAsync(SearchSubscriptionRequest request)
        {
            string uri = $"/v2/subscription/subscriptions{GetQueryParams(request)}";
            return await _restHttpClientV2.GetAsync<ResponsePagingData<SubscriptionResource>>(uri, request, false);
        }

        private static string GetQueryParams(SearchSubscriptionRequest request)
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

            if (!string.IsNullOrEmpty(request.PricingPlanReferenceCode))
            {
                queryParams += "&pricingPlanReferenceCode=" + request.PricingPlanReferenceCode;
            }

            if (!string.IsNullOrEmpty(request.SubscriptionReferenceCode))
            {
                queryParams += "&subscriptionReferenceCode=" + request.SubscriptionReferenceCode;
            }

            if (!string.IsNullOrEmpty(request.ParentReferenceCode))
            {
                queryParams += "&parentReferenceCode=" + request.ParentReferenceCode;
            }

            if (!string.IsNullOrEmpty(request.CustomerReferenceCode))
            {
                queryParams += "&customerReferenceCode=" + request.CustomerReferenceCode;
            }

            if (!string.IsNullOrEmpty(request.SubscriptionStatus))
            {
                queryParams += "&subscriptionStatus=" + request.SubscriptionStatus;
            }

            if (!string.IsNullOrEmpty(request.StartDate))
            {
                queryParams += "&startDate=" + request.StartDate;
            }

            if (!string.IsNullOrEmpty(request.EndDate))
            {
                queryParams += "&endDate=" + request.EndDate;
            }

            if (request.Page != null)
            {
                queryParams += "&page=" + request.Page;
            }

            if (request.Count != null)
            {
                queryParams += "&count=" + request.Count;
            }
            return queryParams;
        }
    }
}