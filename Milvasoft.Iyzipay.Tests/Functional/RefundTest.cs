using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Tests.Functional.Builder.Request;
using Milvasoft.Iyzipay.Tests.Functional.Util;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class RefundTest : BaseTest
    {
        [Test]
        public async Task Should_Refund_Payment()
        {
            CreatePaymentRequest paymentRequest = CreatePaymentRequestBuilder.Create()
                .StandardListingPayment()
                .Build();

            Payment payment = new(RestHttpClient);

            payment = await payment.CreateAsync(paymentRequest).ConfigureAwait(false);

            CreateRefundRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                PaymentTransactionId = payment.PaymentItems[0].PaymentTransactionId,
                Price = "0.2",
                Currency = Currency.TRY.ToString(),
                Ip = "85.34.78.112"
            };

            Refund refund = new(RestHttpClient);

            refund = await refund.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(refund);

            Assert.AreEqual(Status.SUCCESS.ToString(), refund.Status);
            Assert.AreEqual(Locale.TR.ToString(), refund.Locale);
            Assert.AreEqual("123456789", refund.ConversationId);
            Assert.AreEqual(payment.PaymentId, refund.PaymentId);
            Assert.AreEqual(payment.PaymentItems[0].PaymentTransactionId, refund.PaymentTransactionId);
            Assert.AreEqual("0.2", refund.Price.RemoveTrailingZeros());
            Assert.NotNull(refund.SystemTime);
            Assert.Null(refund.ErrorCode);
            Assert.Null(refund.ErrorMessage);
            Assert.Null(refund.ErrorGroup);
        }

        [Test]
        public async Task Should_Refund_Fraudulent_Payment()
        {
            CreatePaymentRequest paymentRequest = CreatePaymentRequestBuilder.Create()
                .StandardListingPayment()
                .Build();

            Payment payment = new(RestHttpClient);

            payment = await payment.CreateAsync(paymentRequest).ConfigureAwait(false);

            CreateRefundRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                PaymentTransactionId = payment.PaymentItems[0].PaymentTransactionId,
                Price = "0.2",
                Currency = Currency.TRY.ToString(),
                Ip = "85.34.78.112",
                Reason = RefundReason.FRAUD.ToString(),
                Description = "stolen card request with 11000 try payment for default sample"
            };

            Refund refund = new(RestHttpClient);

            refund = await refund.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(refund);

            Assert.AreEqual(Status.SUCCESS.ToString(), refund.Status);
            Assert.AreEqual(Locale.TR.ToString(), refund.Locale);
            Assert.AreEqual("123456789", refund.ConversationId);
            Assert.AreEqual(payment.PaymentId, refund.PaymentId);
            Assert.AreEqual(payment.PaymentItems[0].PaymentTransactionId, refund.PaymentTransactionId);
            Assert.AreEqual("0.2", refund.Price.RemoveTrailingZeros());
            Assert.NotNull(refund.SystemTime);
            Assert.Null(refund.ErrorCode);
            Assert.Null(refund.ErrorMessage);
            Assert.Null(refund.ErrorGroup);
        }
    }
}
