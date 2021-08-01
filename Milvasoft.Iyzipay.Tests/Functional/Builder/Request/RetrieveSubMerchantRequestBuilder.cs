using Milvasoft.Iyzipay.Request;

namespace Milvasoft.Iyzipay.Tests.Functional.Builder.Request
{
    public sealed class RetrieveSubMerchantRequestBuilder : BaseRequestBuilder
    {
        private string _subMerchantExternalId;

        private RetrieveSubMerchantRequestBuilder()
        {
        }

        public static RetrieveSubMerchantRequestBuilder Create()
        {
            return new RetrieveSubMerchantRequestBuilder();
        }

        public RetrieveSubMerchantRequestBuilder SubMerchantExternalId(string subMerchantExternalId)
        {
            _subMerchantExternalId = subMerchantExternalId;
            return this;
        }

        public RetrieveSubMerchantRequest Build()
        {
            RetrieveSubMerchantRequest retrieveSubMerchantRequest = new()
            {
                Locale = Locale,
                ConversationId = ConversationId,
                SubMerchantExternalId = _subMerchantExternalId
            };
            return retrieveSubMerchantRequest;
        }
    }
}
