using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Tests.Functional.Builder.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class DisapproveTest : BaseTest
    {
        [Test]
        public async Task Should_Disapprove_Payment()
        {
            CreateSubMerchantRequest request = CreateSubMerchantRequestBuilder.Create()
                .PersonalSubMerchantRequest()
                .Build();

            SubMerchant subMerchant = new(RestHttpClient);

            subMerchant = await subMerchant.CreateAsync(request).ConfigureAwait(false);

            CreatePaymentRequest paymentRequest = CreatePaymentRequestBuilder.Create()
                .MarketplacePayment(subMerchant.SubMerchantKey)
                .Build();

            Payment payment = new(RestHttpClient);

            payment = await payment.CreateAsync(paymentRequest).ConfigureAwait(false);

            string paymentTransactionId = payment.PaymentItems[0].PaymentTransactionId;

            CreateApprovalRequest approvalRequest = CreateApprovalRequestBuilder.Create()
                .PaymentTransactionId(paymentTransactionId)
                .Build();

            Approval approval = new(RestHttpClient);

            await approval.CreateAsync(approvalRequest).ConfigureAwait(false);

            Disapproval disapproval = new(RestHttpClient);

            disapproval = await disapproval.CreateAsync(approvalRequest).ConfigureAwait(false);

            PrintResponse(disapproval);

            Assert.AreEqual(paymentTransactionId, disapproval.PaymentTransactionId);
            Assert.AreEqual(Status.SUCCESS.ToString(), disapproval.Status);
            Assert.AreEqual(Locale.TR.ToString(), disapproval.Locale);
            Assert.NotNull(disapproval.SystemTime);
            Assert.Null(disapproval.ErrorCode);
            Assert.Null(disapproval.ErrorMessage);
            Assert.Null(disapproval.ErrorGroup);
        }
    }
}
