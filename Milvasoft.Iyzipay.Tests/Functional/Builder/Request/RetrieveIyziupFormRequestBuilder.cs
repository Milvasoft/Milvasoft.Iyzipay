using Milvasoft.Iyzipay.Request;

namespace Milvasoft.Iyzipay.Tests.Functional.Builder.Request
{
    public sealed class RetrieveIyziupFormRequestBuilder : BaseRequestBuilder
    {
        private string _token;

        private RetrieveIyziupFormRequestBuilder()
        {
        }

        public static RetrieveIyziupFormRequestBuilder Create()
        {
            return new RetrieveIyziupFormRequestBuilder();
        }

        public RetrieveIyziupFormRequestBuilder Token(string token)
        {
            this._token = token;
            return this;
        }

        public RetrieveIyziupFormRequest Build()
        {
            RetrieveIyziupFormRequest retrieveIyziupFormRequest = new()
            {
                Locale = Locale,
                ConversationId = ConversationId,
                Token = _token
            };
            return retrieveIyziupFormRequest;
        }
    }
}