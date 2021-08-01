using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Tests.Functional.Builder;
using Milvasoft.Iyzipay.Tests.Functional.Builder.Request;
using Milvasoft.Iyzipay.Tests.Functional.Util;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class PaymentTest : BaseTest
    {
        [Test]
        public async Task Should_Create_Listing_Payment()
        {
            CreatePaymentRequest request = CreatePaymentRequestBuilder.Create()
                .StandardListingPayment()
                .Build();

            Payment payment = new(RestHttpClient);

            payment = await payment.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(payment);

            Assert.Null(payment.ConnectorName);
            Assert.AreEqual(Locale.TR.ToString(), payment.Locale);
            Assert.AreEqual(Status.SUCCESS.ToString(), payment.Status);
            Assert.NotNull(payment.SystemTime);
            Assert.Null(payment.ErrorCode);
            Assert.Null(payment.ErrorMessage);
            Assert.Null(payment.ErrorGroup);
            Assert.NotNull(payment.PaymentId);
            Assert.NotNull(payment.BasketId);
            Assert.AreEqual(payment.Price, "1");
            Assert.AreEqual(payment.PaidPrice, "1.1");
            Assert.AreEqual(payment.IyziCommissionRateAmount.RemoveTrailingZeros(), "0.028875");
            Assert.AreEqual(payment.IyziCommissionFee.RemoveTrailingZeros(), "0.25");
            Assert.AreEqual(payment.MerchantCommissionRate.RemoveTrailingZeros(), "10");
            Assert.AreEqual(payment.MerchantCommissionRateAmount.RemoveTrailingZeros(), "0.1");
        }

        [Test]
        public async Task Should_Create_Marketplace_Payment()
        {
            CreateSubMerchantRequest createSubMerchantRequest = CreateSubMerchantRequestBuilder.Create()
                .PersonalSubMerchantRequest()
                .Build();

            SubMerchant subMerchant = new(RestHttpClient);

            string subMerchantKey = (await subMerchant.CreateAsync(createSubMerchantRequest).ConfigureAwait(false)).SubMerchantKey;
            CreatePaymentRequest request = CreatePaymentRequestBuilder.Create()
                .MarketplacePayment(subMerchantKey)
                .Build();

            Payment payment = new(RestHttpClient);

            payment = await payment.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(payment);

            Assert.Null(payment.ConnectorName);
            Assert.AreEqual(Locale.TR.ToString(), payment.Locale);
            Assert.AreEqual(Status.SUCCESS.ToString(), payment.Status);
            Assert.NotNull(payment.SystemTime);
            Assert.Null(payment.ErrorCode);
            Assert.Null(payment.ErrorMessage);
            Assert.Null(payment.ErrorGroup);
            Assert.NotNull(payment.PaymentId);
            Assert.NotNull(payment.BasketId);
            Assert.AreEqual("1", payment.Price);
            Assert.AreEqual("1.1", payment.PaidPrice);
            Assert.AreEqual("0.028875", payment.IyziCommissionRateAmount.RemoveTrailingZeros());
            Assert.AreEqual("0.25", payment.IyziCommissionFee.RemoveTrailingZeros());
            Assert.AreEqual("10", payment.MerchantCommissionRate.RemoveTrailingZeros());
            Assert.AreEqual("0.1", payment.MerchantCommissionRateAmount.RemoveTrailingZeros());
            Assert.AreEqual(1, payment.Installment);
        }

        [Test]
        public async Task Should_Create_Payment_With_Registered_Card()
        {
            string externalUserId = RandomGenerator.RandomId;
            CardInformation cardInformation = CardInformationBuilder.Create()
                .Build();

            CreateCardRequest cardRequest = CreateCardRequestBuilder.Create()
                .Card(cardInformation)
                .ExternalId(externalUserId)
                .Email("email@email.com")
                .Build();

            Card card = new(RestHttpClient);

            card = await card.CreateAsync(cardRequest).ConfigureAwait(false);

            PaymentCard paymentCard = PaymentCardBuilder.Create()
                .CardUserKey(card.CardUserKey)
                .CardToken(card.CardToken)
                .Build();

            CreatePaymentRequest request = CreatePaymentRequestBuilder.Create()
                .StandardListingPayment()
                .PaymentCard(paymentCard)
                .Build();

            Payment payment = new(RestHttpClient);

            payment = await payment.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(payment);

            Assert.Null(payment.ConnectorName);
            Assert.AreEqual(Locale.TR.ToString(), payment.Locale);
            Assert.AreEqual(Status.SUCCESS.ToString(), payment.Status);
            Assert.NotNull(payment.SystemTime);
            Assert.AreEqual("123456789", payment.ConversationId);
            Assert.Null(payment.ErrorCode);
            Assert.Null(payment.ErrorMessage);
            Assert.Null(payment.ErrorGroup);
            Assert.NotNull(payment.PaymentId);
            Assert.NotNull(payment.BasketId);
            Assert.AreEqual("1", payment.Price);
            Assert.AreEqual("1.1", payment.PaidPrice.RemoveTrailingZeros());
            Assert.AreEqual("0.028875", payment.IyziCommissionRateAmount.RemoveTrailingZeros());
            Assert.AreEqual("0.25", payment.IyziCommissionFee.RemoveTrailingZeros());
            Assert.AreEqual("10", payment.MerchantCommissionRate.RemoveTrailingZeros());
            Assert.AreEqual("0.1", payment.MerchantCommissionRateAmount.RemoveTrailingZeros());
            Assert.AreEqual(1, payment.Installment);
        }

        [Test]
        public async Task Should_Retrieve_Payment()
        {
            CreatePaymentRequest request = CreatePaymentRequestBuilder.Create()
                .StandardListingPayment()
                .Build();

            Payment createdPayment = new(RestHttpClient);

            createdPayment = await createdPayment.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(createdPayment);

            RetrievePaymentRequest retrievePaymentRequest = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                PaymentId = createdPayment.PaymentId
            };

            Payment payment = new(RestHttpClient);

            payment = await payment.RetrieveAsync(retrievePaymentRequest).ConfigureAwait(false);

            Assert.AreEqual(Locale.TR.ToString(), payment.Locale);
            Assert.AreEqual(Status.SUCCESS.ToString(), payment.Status);
            Assert.AreEqual(1, payment.Installment);
            Assert.AreEqual("123456789", payment.ConversationId);
            Assert.AreEqual(createdPayment.PaymentId, payment.PaymentId);
            Assert.NotNull(payment.SystemTime);
            Assert.Null(payment.ErrorCode);
            Assert.Null(payment.ErrorMessage);
            Assert.Null(payment.ErrorGroup);
            Assert.NotNull(payment.BasketId);
        }
    }
}