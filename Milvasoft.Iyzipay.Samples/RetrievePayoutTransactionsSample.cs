﻿using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class RetrievePayoutTransactionsSample : Sample
    {
        [Test]
        public async Task Should_Retrieve_Payout_Completed_Transactions()
        {
            RetrieveTransactionsRequest request = new RetrieveTransactionsRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Date = "2015-01-22 19:13:00";

            var payoutCompletedTransactionList = new PayoutCompletedTransactionList(RestHttpClient);

            payoutCompletedTransactionList = await payoutCompletedTransactionList.RetrieveAsync(request).ConfigureAwait(false);

            PrintResponse(payoutCompletedTransactionList);

            Assert.AreEqual(Status.SUCCESS.ToString(), payoutCompletedTransactionList.Status);
            Assert.AreEqual(Locale.TR.ToString(), payoutCompletedTransactionList.Locale);
            Assert.AreEqual("123456789", payoutCompletedTransactionList.ConversationId);
            Assert.IsNotNull(payoutCompletedTransactionList.SystemTime);
            Assert.IsNull(payoutCompletedTransactionList.ErrorCode);
            Assert.IsNull(payoutCompletedTransactionList.ErrorMessage);
            Assert.IsNull(payoutCompletedTransactionList.ErrorGroup);
        }

        [Test]
        public async Task Should_Retrieve_Bounced_Bank_Transfers()
        {
            RetrieveTransactionsRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                Date = "2015-06-02 19:13:00"
            };

            var bouncedBankTransferList = new BouncedBankTransferList(RestHttpClient);

            bouncedBankTransferList = await bouncedBankTransferList.RetrieveAsync(request).ConfigureAwait(false);

            PrintResponse(bouncedBankTransferList);

            Assert.AreEqual(Status.SUCCESS.ToString(), bouncedBankTransferList.Status);
            Assert.AreEqual(Locale.TR.ToString(), bouncedBankTransferList.Locale);
            Assert.AreEqual("123456789", bouncedBankTransferList.ConversationId);
            Assert.IsNotNull(bouncedBankTransferList.SystemTime);
            Assert.IsNull(bouncedBankTransferList.ErrorCode);
            Assert.IsNull(bouncedBankTransferList.ErrorMessage);
            Assert.IsNull(bouncedBankTransferList.ErrorGroup);
        }
    }
}
