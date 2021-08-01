using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class CardManagementPageSample : Sample
    {
        [SetUp]
        public void SetUp()
        {
            Initialize();
            Options.BaseUrl = "https://sandbox-cm.iyzipay.com";
        }

        [Test]
        public async Task Should_Initialize_Card_Management_Page()
        {
            CreateCardManagementPageInitializeRequest request = new()
            {
                CallbackUrl = "https://merchant-callback.com",
                Email = "merchant@email.com",
                ExternalId = "123456789",
                ConversationId = "123456789",
                AddNewCardEnabled = true,
                ValidateNewCard = true,
                DebitCardAllowed = true,
                CardUserKey = "card user key",
                Locale = Locale.TR.ToString()
            };

            var cardManagementPageInitialize = new CardManagementPageInitialize(RestHttpClient);

            cardManagementPageInitialize = await cardManagementPageInitialize.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(cardManagementPageInitialize);

            Assert.AreEqual(Locale.TR.ToString(), cardManagementPageInitialize.Locale);
            Assert.AreEqual(Status.SUCCESS.ToString(), cardManagementPageInitialize.Status);
            Assert.NotNull(cardManagementPageInitialize.SystemTime);
            Assert.AreEqual("123456789", cardManagementPageInitialize.ConversationId);
            Assert.NotNull(cardManagementPageInitialize.Token);
            Assert.NotNull(cardManagementPageInitialize.CardPageUrl);
            Assert.Null(cardManagementPageInitialize.ErrorCode);
            Assert.Null(cardManagementPageInitialize.ErrorMessage);
            Assert.Null(cardManagementPageInitialize.ErrorGroup);
        }
    }
}