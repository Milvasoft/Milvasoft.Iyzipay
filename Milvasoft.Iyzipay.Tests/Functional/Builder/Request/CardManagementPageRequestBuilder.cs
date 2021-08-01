using Milvasoft.Iyzipay.Request;

namespace Milvasoft.Iyzipay.Tests.Functional.Builder.Request
{
    public class CardManagementPageRequestBuilder : BaseRequestBuilder
    {
        private bool _addNewCardEnabled = true;
        private bool _validateNewCard = true;
        private string _externalId = "123123";
        private string _email = "test@iyzico.com";
        private string _cardUserKey;
        private string _callbackUrl = "merchant.com/callbackurl";
        private bool _debitCardAllowed = true;

        public CardManagementPageRequestBuilder()
        {
        }

        public static CardManagementPageRequestBuilder Create()
        {
            return new CardManagementPageRequestBuilder();
        }

        public CardManagementPageRequestBuilder AddNewCardEnabled(bool addNewCardEnabled)
        {
            _addNewCardEnabled = addNewCardEnabled;
            return this;
        }

        public CardManagementPageRequestBuilder ValidateNewCard(bool validateNewCard)
        {
            _validateNewCard = validateNewCard;
            return this;
        }

        public CardManagementPageRequestBuilder ExternalId(string externalId)
        {
            _externalId = externalId;
            return this;
        }

        public CardManagementPageRequestBuilder Email(string email)
        {
            _email = email;
            return this;
        }

        public CardManagementPageRequestBuilder CardUserKey(string cardUserKey)
        {
            _cardUserKey = cardUserKey;
            return this;
        }

        public CardManagementPageRequestBuilder CallbackUrl(string callbackUrl)
        {
            _callbackUrl = callbackUrl;
            return this;
        }

        public CardManagementPageRequestBuilder DebitCardAllowed(bool debitCardAllowed)
        {
            _debitCardAllowed = debitCardAllowed;
            return this;
        }

        public CreateCardManagementPageInitializeRequest Build()
        {
            CreateCardManagementPageInitializeRequest createCardManagementPageInitializeRequest = new()
            {
                AddNewCardEnabled = _addNewCardEnabled,
                CallbackUrl = _callbackUrl,
                CardUserKey = _cardUserKey,
                DebitCardAllowed = _debitCardAllowed,
                ValidateNewCard = _validateNewCard,
                ExternalId = _externalId,
                Email = _email,
                ConversationId = ConversationId,
                Locale = Locale
            };
            return createCardManagementPageInitializeRequest;
        }
    }
}