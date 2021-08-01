using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Tests.Functional.Builder.Request;
using Milvasoft.Iyzipay.Tests.Functional.Util;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class CancelTest : BaseTest
    {
        [Test]
        public async Task Should_Cancel_Payment()
        {
            CreatePaymentRequest paymentRequest = CreatePaymentRequestBuilder.Create()
                .StandardListingPayment()
                .Build();

            Payment payment = new(RestHttpClient);

            payment = await payment.CreateAsync(paymentRequest).ConfigureAwait(false);

            CreateCancelRequest cancelRequest = CreateCancelRequestBuilder.Create()
                .PaymentId(payment.PaymentId)
                .Build();

            Cancel cancel = new(RestHttpClient);

            cancel = await cancel.CreateAsync(cancelRequest).ConfigureAwait(false);

            PrintResponse(cancel);

            Assert.AreEqual(Locale.TR.ToString(), cancel.Locale);
            Assert.AreEqual(Status.SUCCESS.ToString(), cancel.Status);
            Assert.AreEqual(payment.PaymentId, cancel.PaymentId);
            Assert.AreEqual("1.1", cancel.Price.RemoveTrailingZeros());
            Assert.AreEqual(Currency.TRY.ToString(), cancel.Currency);
            Assert.NotNull(cancel.SystemTime);
            Assert.Null(cancel.ErrorCode);
            Assert.Null(cancel.ErrorMessage);
            Assert.Null(cancel.ErrorGroup);
        }

        [Test]
        public async Task Should_Cancel_Fraudulent_Payment()
        {
            CreatePaymentRequest paymentRequest = CreatePaymentRequestBuilder.Create()
                .StandardListingPayment()
                .Build();

            Payment payment = new(RestHttpClient);

            payment = await payment.CreateAsync(paymentRequest).ConfigureAwait(false);

            CreateCancelRequest cancelRequest = CreateCancelRequestBuilder.Create()
                .PaymentId(payment.PaymentId)
                .Build();

            cancelRequest.Reason = RefundReason.FRAUD.ToString();
            cancelRequest.Description = "stolen card request with 11000 try payment for default sample";

            Cancel cancel = new(RestHttpClient);

            cancel = await cancel.CreateAsync(cancelRequest).ConfigureAwait(false);

            PrintResponse(cancel);

            Assert.AreEqual(Locale.TR.ToString(), cancel.Locale);
            Assert.AreEqual(Status.SUCCESS.ToString(), cancel.Status);
            Assert.AreEqual(payment.PaymentId, cancel.PaymentId);
            Assert.AreEqual("1.1", cancel.Price.RemoveTrailingZeros());
            Assert.AreEqual(Currency.TRY.ToString(), cancel.Currency);
            Assert.NotNull(cancel.SystemTime);
            Assert.Null(cancel.ErrorCode);
            Assert.Null(cancel.ErrorMessage);
            Assert.Null(cancel.ErrorGroup);
        }
    }
}
