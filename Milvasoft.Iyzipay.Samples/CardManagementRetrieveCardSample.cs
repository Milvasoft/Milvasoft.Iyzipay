using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class CardManagementRetrieveCardSample : Sample
    {
        [SetUp]
        public void SetUp()
        {
            Initialize();
            Options.BaseUrl = "https://sandbox-cm.iyzipay.com";
        }

        [Test]
        public async Task Should_Retrieve_Card_Management_Page_Cards()
        {
            RetrieveCardManagementPageCardRequest retrieveCardRequest = new()
            {
                PageToken = "set page token",
                Locale = Locale.TR.ToString(),
                ConversationId = "123456"
            };

            var cardManagementPageCard = new CardManagementPageCard(RestHttpClient);

            cardManagementPageCard = await cardManagementPageCard.RetrieveAsync(retrieveCardRequest).ConfigureAwait(false);

            PrintResponse(cardManagementPageCard);

            Assert.AreEqual(Status.SUCCESS.ToString(), cardManagementPageCard.Status);
            Assert.AreEqual(Locale.TR.ToString(), cardManagementPageCard.Locale);
            Assert.NotNull(cardManagementPageCard.SystemTime);
            Assert.Null(cardManagementPageCard.ErrorCode);
            Assert.Null(cardManagementPageCard.ErrorMessage);
            Assert.Null(cardManagementPageCard.ErrorGroup);
            Assert.NotNull(cardManagementPageCard);
        }
    }
}