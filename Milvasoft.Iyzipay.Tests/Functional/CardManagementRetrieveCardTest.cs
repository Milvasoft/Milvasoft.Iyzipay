using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Tests.Functional.Builder.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class CardManagementRetrieveCardTest : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Initialize();
            _options.BaseUrl = "https://sandbox-cm.iyzipay.com";
        }

        [Test]
        public async Task Should_Retrieve_Cards()
        {
            CreateCardManagementPageInitializeRequest initializeRequest = CardManagementPageRequestBuilder.Create().Build();

            CardManagementPageInitialize cardManagementPageInitialize = new(RestHttpClient);

            cardManagementPageInitialize = await cardManagementPageInitialize.CreateAsync(initializeRequest).ConfigureAwait(false);

            RetrieveCardManagementPageCardRequest retrieveCardRequest = CardManagementRetrieveCardBuilder.Create()
                .PageToken(cardManagementPageInitialize.Token)
                .Build();

            CardManagementPageCard cardManagementPageCard = new(RestHttpClient);

            cardManagementPageCard = await cardManagementPageCard.RetrieveAsync(retrieveCardRequest).ConfigureAwait(false);
            PrintResponse(cardManagementPageCard);

            Assert.AreEqual(Status.SUCCESS.ToString(), cardManagementPageCard.Status);
            Assert.AreEqual(Locale.TR.ToString(), cardManagementPageCard.Locale);
            Assert.Null(cardManagementPageCard.ErrorCode);
            Assert.Null(cardManagementPageCard.ErrorMessage);
            Assert.Null(cardManagementPageCard.ErrorGroup);
            Assert.NotNull(cardManagementPageCard);
        }

        [Test]
        public async Task Should_Not_Retrieve_Cards_When_PageToken_Is_Not_Exist()
        {
            RetrieveCardManagementPageCardRequest retrieveCardRequest = CardManagementRetrieveCardBuilder.Create()
                .PageToken("pagetoken")
                .Build();

            CardManagementPageCard cardManagementPageCard = new(RestHttpClient);

            cardManagementPageCard = await cardManagementPageCard.RetrieveAsync(retrieveCardRequest).ConfigureAwait(false);
            PrintResponse(cardManagementPageCard);

            Assert.AreEqual(Status.FAILURE.ToString(), cardManagementPageCard.Status);
            Assert.AreEqual("4002", cardManagementPageCard.ErrorCode);
            Assert.AreEqual("Geçersiz token", cardManagementPageCard.ErrorMessage);
        }
    }
}