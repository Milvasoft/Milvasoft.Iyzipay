using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Model.V2.Transaction;
using Milvasoft.Iyzipay.Request.V2;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class RetrieveTransactionDetailSample : Sample
    {
        [Test]
        public async Task Should_Retrieve_Transaction()
        {
            RetrieveTransactionDetailRequest request = new()
            {
                PaymentConversationId = "payment123456789x"
            };

            var transactionDetail = new TransactionDetail(RestHttpClientV2);

            transactionDetail = await transactionDetail.Retrieve(request).ConfigureAwait(false);

            PrintResponse(transactionDetail);

            Assert.AreEqual(Status.SUCCESS.ToString(), transactionDetail.Status);
            Assert.IsNotNull(transactionDetail.SystemTime);
            Assert.IsNull(transactionDetail.ErrorMessage);
        }
    }


}
