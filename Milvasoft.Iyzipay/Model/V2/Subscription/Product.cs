using Milvasoft.Iyzipay.Request.V2.Subscription;
using Milvasoft.Iyzipay.Utils.Abstract;
using Milvasoft.Iyzipay.Utils.Concrete;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Model.V2.Subscription
{
    public class Product : IyzipayResourceV2
    {
        private readonly IRestHttpClientV2 _restHttpClientV2;

        public Product(IRestHttpClientV2 restHttpClientV2)
        {
            _restHttpClientV2 = restHttpClientV2;
        }

        public async Task<ResponseData<ProductResource>> Create(CreateProductRequest request)
        {
            string uri = $"/v2/subscription/products";
            return await _restHttpClientV2.PostAsync<ResponseData<ProductResource>>(uri, request, true).ConfigureAwait(false);
        }

        public async Task<ResponseData<ProductResource>> Update(UpdateProductRequest request)
        {
            string uri = $"/v2/subscription/products/{request.ProductReferenceCode}";
            return await _restHttpClientV2.PostAsync<ResponseData<ProductResource>>(uri, request, true).ConfigureAwait(false);
        }

        public async Task<IyzipayResourceV2> Delete(DeleteProductRequest request)
        {
            string uri = $"/v2/subscription/products/{request.ProductReferenceCode}";
            return await _restHttpClientV2.DeleteAsync<IyzipayResourceV2>(uri, request, true).ConfigureAwait(false);
        }

        public async Task<ResponseData<ProductResource>> Retrieve(RetrieveProductRequest request)
        {
            string uri = $"/v2/subscription/products/{request.ProductReferenceCode}";
            return await _restHttpClientV2.GetAsync<ResponseData<ProductResource>>(uri, request, false).ConfigureAwait(false);
        }

        public async Task<ResponsePagingData<ProductResource>> RetrieveAll(PagingRequest request)
        {
            string uri = $"/v2/subscription/products{GetQueryParams(request)}";
            return await _restHttpClientV2.GetAsync<ResponsePagingData<ProductResource>>(uri, request, false).ConfigureAwait(false);
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