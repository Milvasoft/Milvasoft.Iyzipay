using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Utils.Concrete;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class ApiTest : BaseTest
    {
        [Test]
        public async Task Should_Test_Api()
        {
            var apiTest = new Model.ApiTest(RestHttpClient);

            IyzipayResource iyzipayResource = await apiTest.RetrieveAsync().ConfigureAwait(false);

            PrintResponse(iyzipayResource);

            Assert.AreEqual(Status.SUCCESS.ToString(), iyzipayResource.Status);
            Assert.AreEqual(Locale.TR.ToString(), iyzipayResource.Locale);
            Assert.NotNull(iyzipayResource.SystemTime);
            Assert.Null(iyzipayResource.ErrorCode);
            Assert.Null(iyzipayResource.ErrorMessage);
            Assert.Null(iyzipayResource.ErrorGroup);
        }
    }
}
