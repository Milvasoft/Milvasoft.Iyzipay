using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Tests.Functional.Builder.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class ApprovalTest : BaseTest
    {
        [Test]
        public async Task Should_Approve_Payment_Item()
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

            approval = await approval.CreateAsync(approvalRequest).ConfigureAwait(false);

            Assert.AreEqual(paymentTransactionId, approval.PaymentTransactionId);
            Assert.AreEqual(Locale.TR.ToString(), payment.Locale);
            Assert.AreEqual(Status.SUCCESS.ToString(), payment.Status);
            Assert.NotNull(payment.SystemTime);
            Assert.Null(payment.ErrorCode);
            Assert.Null(payment.ErrorMessage);
            Assert.Null(payment.ErrorGroup);
        }
    }
}
