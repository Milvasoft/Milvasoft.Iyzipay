﻿using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class BinNumberSample : Sample
    {
        [Test]
        public async Task Should_Retrieve_Bin_Number()
        {
            RetrieveBinNumberRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                BinNumber = "554960"
            };

            var binNumber = new BinNumber(RestHttpClient);

            binNumber = await binNumber.RetrieveAsync(request).ConfigureAwait(false);

            PrintResponse(binNumber);

            Assert.AreEqual(Status.SUCCESS.ToString(), binNumber.Status);
            Assert.AreEqual(Locale.TR.ToString(), binNumber.Locale);
            Assert.AreEqual("123456789", binNumber.ConversationId);
            Assert.IsNotNull(binNumber.SystemTime);
            Assert.IsNull(binNumber.ErrorCode);
            Assert.IsNull(binNumber.ErrorMessage);
            Assert.IsNull(binNumber.ErrorGroup);
            Assert.AreEqual("554960", binNumber.Bin);
            Assert.AreEqual("CREDIT_CARD", binNumber.CardType);
            Assert.AreEqual("MASTER_CARD", binNumber.CardAssociation);
            Assert.AreEqual("Bonus", binNumber.CardFamily);
            Assert.AreEqual("Garanti Bankası", binNumber.BankName);
            Assert.AreEqual(62, binNumber.BankCode);
        }
    }
}