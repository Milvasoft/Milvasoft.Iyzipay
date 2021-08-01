using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class DisapproveSample : Sample
    {
        [Test]
        public async Task Should_Disapprove_Payment_Item()
        {
            CreateApprovalRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                PaymentTransactionId = "1"
            };

            var disapproval = new Disapproval(RestHttpClient);

            disapproval = await disapproval.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(disapproval);

            Assert.AreEqual(Status.SUCCESS.ToString(), disapproval.Status);
            Assert.AreEqual(Locale.TR.ToString(), disapproval.Locale);
            Assert.AreEqual("123456789", disapproval.ConversationId);
            Assert.IsNotNull(disapproval.SystemTime);
            Assert.IsNull(disapproval.ErrorCode);
            Assert.IsNull(disapproval.ErrorMessage);
            Assert.IsNull(disapproval.ErrorGroup);
        }
    }
}
