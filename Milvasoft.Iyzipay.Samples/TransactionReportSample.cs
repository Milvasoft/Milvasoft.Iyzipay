using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Model.V2.Transaction;
using Milvasoft.Iyzipay.Request.V2;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class TransactionReportSample : Sample
    {
        [Test]
        public async Task Should_Retrieve_TransactionReport()
        {
            RetrieveTransactionReportRequest request = new()
            {
                ConversationId = "123",
                TransactionDate = "2018-06-28",
                Page = 1
            };

            var transactionReport = new TransactionReport(RestHttpClientV2);

            transactionReport = await transactionReport.RetrieveAsync(request).ConfigureAwait(false);

            PrintResponse(transactionReport);

            Assert.AreEqual(Status.SUCCESS.ToString(), transactionReport.Status);
            Assert.AreEqual(200, transactionReport.StatusCode);
            Assert.AreEqual("123", transactionReport.ConversationId);
            Assert.AreEqual(1, transactionReport.CurrentPage);
            Assert.IsNotNull(transactionReport.TotalPageCount);
            Assert.IsNotNull(transactionReport.SystemTime);
            Assert.IsNull(transactionReport.ErrorMessage);
        }
    }
}
