using Milvasoft.Iyzipay.Request;

namespace Milvasoft.Iyzipay.Tests.Functional.Builder.Request
{
    public sealed class RetrieveCheckoutFormRequestBuilder : BaseRequestBuilder
    {
        private string _token;

        private RetrieveCheckoutFormRequestBuilder()
        {
        }

        public static RetrieveCheckoutFormRequestBuilder Create()
        {
            return new RetrieveCheckoutFormRequestBuilder();
        }

        public RetrieveCheckoutFormRequestBuilder Token(string token)
        {
            this._token = token;
            return this;
        }

        public RetrieveCheckoutFormRequest Build()
        {
            RetrieveCheckoutFormRequest retrieveCheckoutFormRequest = new()
            {
                Locale = Locale,
                ConversationId = ConversationId,
                Token = _token
            };
            return retrieveCheckoutFormRequest;
        }
    }
}
