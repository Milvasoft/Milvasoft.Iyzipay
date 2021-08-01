using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class ApproveSample : Sample
    {
        [Test]
        public async Task Should_Approve_Payment_Item()
        {
            CreateApprovalRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                PaymentTransactionId = "1"
            };

            var approval = new Approval(RestHttpClient);

            approval = await approval.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(approval);

            Assert.AreEqual(Status.SUCCESS.ToString(), approval.Status);
            Assert.AreEqual(Locale.TR.ToString(), approval.Locale);
            Assert.AreEqual("123456789", approval.ConversationId);
            Assert.IsNotNull(approval.SystemTime);
            Assert.IsNull(approval.ErrorCode);
            Assert.IsNull(approval.ErrorMessage);
            Assert.IsNull(approval.ErrorGroup);
        }
    }
}
