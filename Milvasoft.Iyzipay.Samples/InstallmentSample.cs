using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class InstallmentSample : Sample
    {
        [Test]
        public async Task Should_Retrieve_Installments()
        {
            RetrieveInstallmentInfoRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                BinNumber = "554960",
                Price = "100"
            };

            var installmentInfo = new InstallmentInfo(RestHttpClient);

            installmentInfo = await installmentInfo.RetrieveAsync(request).ConfigureAwait(false);

            PrintResponse(installmentInfo);

            Assert.AreEqual(Status.SUCCESS.ToString(), installmentInfo.Status);
            Assert.AreEqual(Locale.TR.ToString(), installmentInfo.Locale);
            Assert.AreEqual("123456789", installmentInfo.ConversationId);
            Assert.IsNotNull(installmentInfo.SystemTime);
            Assert.IsNull(installmentInfo.ErrorCode);
            Assert.IsNull(installmentInfo.ErrorMessage);
            Assert.IsNull(installmentInfo.ErrorGroup);
            Assert.IsNotNull(installmentInfo.InstallmentDetails);
            Assert.IsNotEmpty(installmentInfo.InstallmentDetails);
        }
    }
}
