using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Tests.Functional.Builder.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class ThreedsTest : BaseTest
    {
        [Test]
        public async Task Should_Create_Payment_With_Physical_And_Virtual_Item_For_Standard_Merchant()
        {
            CreatePaymentRequest createPaymentRequest = CreatePaymentRequestBuilder.Create()
                .StandardListingPayment()
                .CallbackUrl("https://www.merchant.com/callback")
                .Build();

            ThreedsInitialize threedsInitialize = new(RestHttpClient);

            threedsInitialize = await threedsInitialize.CreateAsync(createPaymentRequest).ConfigureAwait(false);

            PrintResponse(threedsInitialize);

            Assert.AreEqual(Locale.TR.ToString(), threedsInitialize.Locale);
            Assert.AreEqual(Status.SUCCESS.ToString(), threedsInitialize.Status);
            Assert.NotNull(threedsInitialize.SystemTime);
            Assert.NotNull(threedsInitialize.HtmlContent);
            Assert.Null(threedsInitialize.ErrorCode);
            Assert.Null(threedsInitialize.ErrorMessage);
            Assert.Null(threedsInitialize.ErrorGroup);
        }

        [Test]
        public async Task Should_Create_Threeds_Payment_With_Physical_And_Virtual_Item_For_Marketplace_Merchant()
        {
            CreateSubMerchantRequest createSubMerchantRequest = CreateSubMerchantRequestBuilder.Create()
                .PersonalSubMerchantRequest()
                .Build();

            SubMerchant subMerchant = new(RestHttpClient);

            subMerchant = await subMerchant.CreateAsync(createSubMerchantRequest).ConfigureAwait(false);

            CreatePaymentRequest createPaymentRequest = CreatePaymentRequestBuilder.Create()
                .MarketplacePayment(subMerchant.SubMerchantKey)
                .CallbackUrl("https://www.merchant.com/callback")
                .Build();

            ThreedsInitialize threedsInitialize = new(RestHttpClient);

            threedsInitialize = await threedsInitialize.CreateAsync(createPaymentRequest).ConfigureAwait(false);

            PrintResponse(threedsInitialize);

            Assert.AreEqual(Locale.TR.ToString(), threedsInitialize.Locale);
            Assert.AreEqual(Status.SUCCESS.ToString(), threedsInitialize.Status);
            Assert.NotNull(threedsInitialize.SystemTime);
            Assert.NotNull(threedsInitialize.HtmlContent);
            Assert.Null(threedsInitialize.ErrorCode);
            Assert.Null(threedsInitialize.ErrorMessage);
            Assert.Null(threedsInitialize.ErrorGroup);
        }

        /*
            This test needs manual payment from Pecco on sandbox environment. So it does not contain any assertions.
        */
        [Test]
        public async Task Should_Auth_Threeds()
        {
            CreateThreedsPaymentRequest createThreedsPaymentRequest = new()
            {
                ConversationData = "conversion data",
                PaymentId = "1",
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789"
            };

            ThreedsPayment threedsPayment = new(RestHttpClient);

            threedsPayment = await threedsPayment.CreateAsync(createThreedsPaymentRequest).ConfigureAwait(false);

            PrintResponse(threedsPayment);
        }
    }
}