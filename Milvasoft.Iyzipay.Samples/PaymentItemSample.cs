using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class PaymentItemSample : Sample
    {
        [Test]
        public async Task Should_Update_Payment_Item()
        {
            UpdatePaymentItemRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubMerchantKey = "subMerchantKey",
                PaymentTransactionId = "transactionId",
                SubMerchantPrice = "price"
            };

            var paymentItem = new PaymentItem(RestHttpClient);

            paymentItem = await paymentItem.UpdateAsync(request).ConfigureAwait(false);

            PrintResponse(paymentItem);

            Assert.AreEqual(Status.SUCCESS.ToString(), paymentItem.Status);
            Assert.AreEqual(Locale.TR.ToString(), paymentItem.Locale);
            Assert.AreEqual("123456789", paymentItem.ConversationId);
            Assert.IsNotNull(paymentItem.SystemTime);
            Assert.IsNull(paymentItem.ErrorCode);
            Assert.IsNull(paymentItem.ErrorMessage);
            Assert.IsNull(paymentItem.ErrorGroup);
        }
    }
}
