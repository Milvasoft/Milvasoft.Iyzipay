using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Tests.Functional.Builder.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class CardManagementPageTest : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Initialize();
            _options.BaseUrl = "https://sandbox-cm.iyzipay.com";
        }


        [Test]
        public async Task Should_Initialize_Card_Management_Page()
        {
            CreateCardManagementPageInitializeRequest request = CardManagementPageRequestBuilder.Create().Build();

            CardManagementPageInitialize cardManagementPageInitialize = new(RestHttpClient);

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

        [Test]
        public async Task Should_Not_Initialize_Card_Management_Page_When_CallbackUrl_Not_Exist()
        {
            CreateCardManagementPageInitializeRequest request = CardManagementPageRequestBuilder.Create().CallbackUrl("").Build();

            CardManagementPageInitialize cardManagementPageInitialize = new(RestHttpClient);

            cardManagementPageInitialize = await cardManagementPageInitialize.CreateAsync(request).ConfigureAwait(false);
            PrintResponse(cardManagementPageInitialize);

            Assert.AreEqual(Status.FAILURE.ToString(), cardManagementPageInitialize.Status);
            Assert.Null(cardManagementPageInitialize.ExternalId);
            Assert.Null(cardManagementPageInitialize.ConversationId);
            Assert.Null(cardManagementPageInitialize.ErrorGroup);
            Assert.Null(cardManagementPageInitialize.Token);
            Assert.Null(cardManagementPageInitialize.CardPageUrl);
            Assert.AreEqual("Callback url gönderilmesi zorunludur", cardManagementPageInitialize.ErrorMessage);
            Assert.AreEqual("23", cardManagementPageInitialize.ErrorCode);
        }
    }
}