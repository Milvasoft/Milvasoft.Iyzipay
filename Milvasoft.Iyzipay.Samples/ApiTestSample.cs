using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Utils.Concrete;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class ApiTestSample : Sample
    {
        [Test]
        public async Task Should_Test_Api()
        {
            var apiTest = new ApiTest(RestHttpClient);

            IyzipayResource iyzipayResource = await apiTest.RetrieveAsync().ConfigureAwait(false);

            PrintResponse(iyzipayResource);

            Assert.AreEqual(Status.SUCCESS.ToString(), iyzipayResource.Status);
            Assert.AreEqual(Locale.TR.ToString(), iyzipayResource.Locale);
            Assert.IsNotNull(iyzipayResource.SystemTime);
            Assert.IsNull(iyzipayResource.ErrorCode);
            Assert.IsNull(iyzipayResource.ErrorMessage);
            Assert.IsNull(iyzipayResource.ErrorGroup);

        }
    }
}
