using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Tests.Functional.Builder.Request;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class BkmTest : BaseTest
    {
        [Test]
        public async Task Should_Initialize_Bkm()
        {
            CreateBkmInitializeRequest request = CreateBkmInitializeRequestBuilder.Create()
                .Price("1")
                .CallbackUrl("https://www.merchant.com/callback")
                .Build();

            BkmInitialize bkmInitialize = new(RestHttpClient);

            bkmInitialize = await bkmInitialize.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(request);

            Assert.NotNull(bkmInitialize.HtmlContent);
            Assert.NotNull(bkmInitialize.Token);
            Assert.True(bkmInitialize.HtmlContent.Contains(bkmInitialize.Token));
        }
    }
}
