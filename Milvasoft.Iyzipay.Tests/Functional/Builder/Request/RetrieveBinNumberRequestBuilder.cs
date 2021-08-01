using Milvasoft.Iyzipay.Request;

namespace Milvasoft.Iyzipay.Tests.Functional.Builder.Request
{
    public sealed class RetrieveBinNumberRequestBuilder : BaseRequestBuilder
    {
        private string _binNumber;

        private RetrieveBinNumberRequestBuilder()
        {
        }

        public static RetrieveBinNumberRequestBuilder Create()
        {
            return new RetrieveBinNumberRequestBuilder();
        }

        public RetrieveBinNumberRequestBuilder BinNumber(string binNumber)
        {
            this._binNumber = binNumber;
            return this;
        }

        public RetrieveBinNumberRequest Build()
        {
            RetrieveBinNumberRequest retrieveBinNumberRequest = new()
            {
                Locale = Locale,
                ConversationId = ConversationId,
                BinNumber = _binNumber
            };
            return retrieveBinNumberRequest;
        }
    }
}
