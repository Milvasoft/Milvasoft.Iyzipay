using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class RefundSample : Sample
    {
        [Test]
        public async Task Should_Refund()
        {
            CreateRefundRequest request = new()
            {
                ConversationId = "123456789",
                Locale = Locale.TR.ToString(),
                PaymentTransactionId = "1",
                Price = "0.5",
                Ip = "85.34.78.112",
                Currency = Currency.TRY.ToString()
            };

            var refund = new Refund(RestHttpClient);

            refund = await refund.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(refund);

            Assert.AreEqual(Status.SUCCESS.ToString(), refund.Status);
            Assert.AreEqual(Locale.TR.ToString(), refund.Locale);
            Assert.AreEqual("123456789", refund.ConversationId);
            Assert.IsNotNull(refund.SystemTime);
            Assert.IsNull(refund.ErrorCode);
            Assert.IsNull(refund.ErrorMessage);
            Assert.IsNull(refund.ErrorGroup);
        }

        [Test]
        public async Task Should_Amount_Based_Refund()
        {
            CreateAmountBasedRefundRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "--",
                Ip = "85.34.78.112",
                Price = "2",
                PaymentId = "12425590"
            };

            var amountBasedRefund = new Refund(RestHttpClient);

            amountBasedRefund = await amountBasedRefund.CreateAmountBasedRefundRequestAsync(request).ConfigureAwait(false);

            PrintResponse(amountBasedRefund);

            Assert.AreEqual(Status.SUCCESS.ToString(), amountBasedRefund.Status);
            Assert.AreEqual("10", amountBasedRefund.Price);
            Assert.AreEqual(Locale.TR.ToString(), amountBasedRefund.Locale);
            Assert.AreEqual("--", amountBasedRefund.ConversationId);
            Assert.IsNotNull(amountBasedRefund.SystemTime);
            Assert.IsNull(amountBasedRefund.ErrorCode);
            Assert.IsNull(amountBasedRefund.ErrorMessage);
            Assert.IsNull(amountBasedRefund.ErrorGroup);
        }


        public async Task Should_Refund_With_Reason_And_Description()
        {
            CreateRefundRequest request = new()
            {
                ConversationId = "123456789",
                Locale = Locale.TR.ToString(),
                PaymentTransactionId = "1",
                Price = "0.5",
                Ip = "85.34.78.112",
                Currency = Currency.TRY.ToString(),
                Reason = RefundReason.OTHER.ToString(),
                Description = "customer requested for default sample"
            };

            var refund = new Refund(RestHttpClient);

            refund = await refund.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(refund);

            Assert.AreEqual(Status.SUCCESS.ToString(), refund.Status);
            Assert.AreEqual(Locale.TR.ToString(), refund.Locale);
            Assert.AreEqual("123456789", refund.ConversationId);
            Assert.IsNotNull(refund.SystemTime);
            Assert.IsNull(refund.ErrorCode);
            Assert.IsNull(refund.ErrorMessage);
            Assert.IsNull(refund.ErrorGroup);
        }

    }
}
