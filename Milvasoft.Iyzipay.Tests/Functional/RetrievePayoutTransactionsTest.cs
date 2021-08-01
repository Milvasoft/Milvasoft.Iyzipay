using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class RetrievePayoutTransactionsTest : BaseTest
    {
        [Test]
        public async Task Should_Retrieve_Payout_Completed_Transactions()
        {
            RetrieveTransactionsRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                Date = "2016-01-22 19:13:00"
            };

            PayoutCompletedTransactionList payoutCompletedTransactionList = new(RestHttpClient);

            payoutCompletedTransactionList = await payoutCompletedTransactionList.RetrieveAsync(request).ConfigureAwait(false);

            PrintResponse(payoutCompletedTransactionList);

            Assert.AreEqual(Status.SUCCESS.ToString(), payoutCompletedTransactionList.Status);
            Assert.AreEqual(Locale.TR.ToString(), payoutCompletedTransactionList.Locale);
            Assert.AreEqual("123456789", payoutCompletedTransactionList.ConversationId);
            Assert.NotNull(payoutCompletedTransactionList.SystemTime);
            Assert.Null(payoutCompletedTransactionList.ErrorCode);
            Assert.Null(payoutCompletedTransactionList.ErrorGroup);
            Assert.Null(payoutCompletedTransactionList.ErrorMessage);
        }

        [Test]
        public async Task Should_Retrieve_Bounced_Bank_Transfers()
        {
            RetrieveTransactionsRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                Date = "2016-01-22 19:13:00"
            };

            BouncedBankTransferList bouncedBankTransferList = new(RestHttpClient);

            bouncedBankTransferList = await bouncedBankTransferList.RetrieveAsync(request).ConfigureAwait(false);

            PrintResponse(bouncedBankTransferList);

            Assert.AreEqual(Status.SUCCESS.ToString(), bouncedBankTransferList.Status);
            Assert.AreEqual(Locale.TR.ToString(), bouncedBankTransferList.Locale);
            Assert.AreEqual("123456789", bouncedBankTransferList.ConversationId);
            Assert.NotNull(bouncedBankTransferList.SystemTime);
            Assert.Null(bouncedBankTransferList.ErrorCode);
            Assert.Null(bouncedBankTransferList.ErrorGroup);
            Assert.Null(bouncedBankTransferList.ErrorMessage);
        }
    }
}
