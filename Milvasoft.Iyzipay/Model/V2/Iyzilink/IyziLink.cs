using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model.V2.Iyzilink
{
    public class IyziLink : IyzipayResourceV2
    {
        private static readonly string V2_IYZILINK_PRODUCTS = $"/v2/iyzilink/products";
        private readonly IRestHttpClientV2 _restHttpClientV2;

        public IyziLink(IRestHttpClientV2 restHttpClientV2)
        {
            _restHttpClientV2 = restHttpClientV2;
        }

        public async Task<ResponseData<IyziLinkSave>> CreateAsync(IyziLinkSaveRequest request)
        {
            string uri = V2_IYZILINK_PRODUCTS + GetQueryParams(request);
            return await _restHttpClientV2.PostAsync<ResponseData<IyziLinkSave>>(uri, request, true).ConfigureAwait(false);
        }

        public async Task<ResponseData<IyziLinkSave>> UpdateAsync(string token, IyziLinkSaveRequest request)
        {
            string uri = V2_IYZILINK_PRODUCTS + "/" + token + GetQueryParams(request);
            return await _restHttpClientV2.PutAsync<ResponseData<IyziLinkSave>>(uri, request, true).ConfigureAwait(false);
        }

        public async Task<ResponsePagingData<IyziLinkItem>> RetrieveAllAsync(PagingRequest request)
        {
            string queryParams = GetQueryParams(request);
            string iyzilinkQueryParam = "productType=IYZILINK";

            queryParams = string.IsNullOrEmpty(queryParams)
                ? "?" + iyzilinkQueryParam
                : queryParams + "&" + iyzilinkQueryParam;

            string uri = V2_IYZILINK_PRODUCTS + queryParams;
            return await _restHttpClientV2.GetAsync<ResponsePagingData<IyziLinkItem>>(uri, request, false).ConfigureAwait(false);
        }

        public async Task<ResponseData<IyziLinkItem>> RetrieveAsync(string token, BaseRequestV2 request)
        {
            string uri = V2_IYZILINK_PRODUCTS + "/" + token + GetQueryParams(request);
            return await _restHttpClientV2.GetAsync<ResponseData<IyziLinkItem>>(uri, request, false).ConfigureAwait(false);
        }

        public async Task<IyzipayResourceV2> DeleteAsync(string token, BaseRequestV2 request)
        {
            string uri = V2_IYZILINK_PRODUCTS + "/" + token + GetQueryParams(request);
            return await _restHttpClientV2.DeleteAsync<IyzipayResourceV2>(uri, request, true).ConfigureAwait(false);
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