using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Model.V2;
using Milvasoft.Iyzipay.Model.V2.Iyzilink;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Utils.Concrete;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class IyziLinkSample : Sample
    {
        [Test]
        public async Task Should_Create_IyziLink()
        {
            IyziLinkSaveRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                Name = "ft-name",
                Description = "ft-description",
                Base64EncodedImage = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mP8H8BwGwAF0QIs4BDpAAAAAABJRU5ErkJggg==",
                Price = "1",
                Currency = Currency.TRY.ToString(),
                AddressIgnorable = false,
                SoldLimit = 1,
                InstallmentRequested = false
            };

            var iyziLink = new IyziLink(RestHttpClientV2);

            ResponseData<IyziLinkSave> response = await iyziLink.Create(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.AreEqual(Locale.TR.ToString(), response.Locale);
            Assert.AreEqual("123456789", response.ConversationId);
            Assert.NotNull(response.SystemTime);
            Assert.NotNull(response.Data.Url);
            Assert.NotNull(response.Data.ImageUrl);
            Assert.NotNull(response.Data.Token);
        }

        [Test]
        public async Task Should_Update_IyziLink()
        {
            IyziLinkSaveRequest updateRequest = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                Name = "ft-name-updated",
                Description = "ft-description-updated",
                Base64EncodedImage = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mP8H8BwGwAF0QIs4BDpAAAAAABJRU5ErkJggg==",
                Price = "10",
                Currency = Currency.TRY.ToString(),
                AddressIgnorable = false,
                SoldLimit = 1,
                InstallmentRequested = false
            };

            var iyziLink = new IyziLink(RestHttpClientV2);

            ResponseData<IyziLinkSave> response = await iyziLink.Update("token", updateRequest).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.AreEqual(Locale.TR.ToString(), response.Locale);
            Assert.AreEqual("123456789", response.ConversationId);
            Assert.NotNull(response.SystemTime);
            Assert.NotNull(response.Data.Url);
            Assert.NotNull(response.Data.ImageUrl);
            Assert.NotNull(response.Data.Token);
        }

        [Test]
        public async Task Should_Retrieve_IyziLinks_With_Pagination()
        {
            PagingRequest pagingRequest = new()
            {
                Page = 1,
                Count = 1,
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789"
            };

            var iyziLink = new IyziLink(RestHttpClientV2);

            ResponsePagingData<IyziLinkItem> response = await iyziLink.RetrieveAll(pagingRequest).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.AreEqual(Locale.TR.ToString(), response.Locale);
            Assert.AreEqual("123456789", response.ConversationId);
            Assert.NotNull(response.SystemTime);
            Assert.AreEqual(1, response.Data.Items.Count);
            Assert.AreEqual(1, response.Data.CurrentPage);
        }

        [Test]
        public async Task Should_Retrieve_IyziLink_With_Token()
        {
            BaseRequestV2 requestV2 = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789"
            };

            var iyziLink = new IyziLink(RestHttpClientV2);

            ResponseData<IyziLinkItem> response = await iyziLink.Retrieve("token", requestV2).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.AreEqual(Locale.TR.ToString(), response.Locale);
            Assert.AreEqual("123456789", response.ConversationId);
            Assert.NotNull(response.SystemTime);
            Assert.AreEqual("ft-name", response.Data.Name);
            Assert.AreEqual("ft-description", response.Data.Description);
            Assert.AreEqual(Currency.TRY.ToString(), response.Data.Currency);
            Assert.AreEqual(IyziLinkStatus.ACTIVE, response.Data.IyziLinkStatus);
            Assert.AreEqual(false, response.Data.AddressIgnorable);
            Assert.NotNull(response.Data.ImageUrl);
        }

        [Test]
        public async Task Should_Delete_IyziLink()
        {
            BaseRequestV2 requestV2 = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789"
            };

            var iyziLink = new IyziLink(RestHttpClientV2);

            IyzipayResourceV2 response = await iyziLink.Delete("token", requestV2).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.AreEqual(Locale.TR.ToString(), response.Locale);
            Assert.AreEqual("123456789", response.ConversationId);
            Assert.NotNull(response.SystemTime);
        }
    }

}