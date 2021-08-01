using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class CancelSample : Sample
    {
        [Test]
        public async Task Should_Cancel_Payment()
        {
            CreateCancelRequest request = new()
            {
                ConversationId = "123456789",
                Locale = Locale.TR.ToString(),
                PaymentId = "1",
                Ip = "85.34.78.112"
            };

            var cancel = new Cancel(RestHttpClient);

            cancel = await cancel.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(cancel);

            Assert.AreEqual(Status.SUCCESS.ToString(), cancel.Status);
            Assert.AreEqual(Locale.TR.ToString(), cancel.Locale);
            Assert.AreEqual("123456789", cancel.ConversationId);
            Assert.IsNotNull(cancel.SystemTime);
            Assert.IsNull(cancel.ErrorCode);
            Assert.IsNull(cancel.ErrorMessage);
            Assert.IsNull(cancel.ErrorGroup);
        }

        [Test]
        public async Task Should_Cancel_Payment_With_Reason_And_Description()
        {
            CreateCancelRequest request = new()
            {
                ConversationId = "123456789",
                Locale = Locale.TR.ToString(),
                PaymentId = "1",
                Ip = "85.34.78.112",
                Reason = RefundReason.OTHER.ToString(),
                Description = "customer requested for default sample"
            };

            var cancel = new Cancel(RestHttpClient);

            cancel = await cancel.CreateAsync(request);

            PrintResponse(cancel);

            Assert.AreEqual(Status.SUCCESS.ToString(), cancel.Status);
            Assert.AreEqual(Locale.TR.ToString(), cancel.Locale);
            Assert.AreEqual("123456789", cancel.ConversationId);
            Assert.IsNotNull(cancel.SystemTime);
            Assert.IsNull(cancel.ErrorCode);
            Assert.IsNull(cancel.ErrorMessage);
            Assert.IsNull(cancel.ErrorGroup);
        }
    }
}
