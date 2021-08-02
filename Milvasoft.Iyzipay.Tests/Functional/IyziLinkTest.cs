using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Model.V2;
using Milvasoft.Iyzipay.Model.V2.Iyzilink;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Tests.Functional.Util;
using Milvasoft.Iyzipay.Utils.Concrete;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class IyziLinkTest : BaseTest
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

            IyziLink iyziLink = new(RestHttpClientV2);

            ResponseData<IyziLinkSave> response = await iyziLink.CreateAsync(request).ConfigureAwait(false);

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

            IyziLink iyziLink = new(RestHttpClientV2);

            string token = (await iyziLink.CreateAsync(request).ConfigureAwait(false)).Data.Token;

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

            ResponseData<IyziLinkSave> response = await iyziLink.UpdateAsync(token, updateRequest).ConfigureAwait(false);

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

            IyziLink iyziLink = new(RestHttpClientV2);

            await iyziLink.CreateAsync(request).ConfigureAwait(false);

            PagingRequest pagingRequest = new()
            {
                Page = 1,
                Count = 1,
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789"
            };

            ResponsePagingData<IyziLinkItem> response = await iyziLink.RetrieveAllAsync(pagingRequest).ConfigureAwait(false);

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

            IyziLink iyziLink = new(RestHttpClientV2);

            string token = (await iyziLink.CreateAsync(request).ConfigureAwait(false)).Data.Token;

            BaseRequestV2 requestV2 = new();
            requestV2.Locale = Locale.TR.ToString();
            requestV2.ConversationId = "123456789";

            ResponseData<IyziLinkItem> response = await iyziLink.RetrieveAsync(token, requestV2).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.AreEqual(Locale.TR.ToString(), response.Locale);
            Assert.AreEqual("123456789", response.ConversationId);
            Assert.NotNull(response.SystemTime);
            Assert.AreEqual("ft-name", response.Data.Name);
            Assert.AreEqual("ft-description", response.Data.Description);
            Assert.AreEqual("1", response.Data.Price.RemoveTrailingZeros());
            Assert.AreEqual(Currency.TRY.ToString(), response.Data.Currency);
            Assert.AreEqual(token, response.Data.Token);
            Assert.AreEqual(IyziLinkStatus.ACTIVE, response.Data.IyziLinkStatus);
            Assert.AreEqual(false, response.Data.AddressIgnorable);
            Assert.NotNull(response.Data.ImageUrl);
        }

        [Test]
        public async Task Should_Delete_IyziLink()
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

            IyziLink iyziLink = new(RestHttpClientV2);

            string token = (await iyziLink.CreateAsync(request).ConfigureAwait(false)).Data.Token;

            BaseRequestV2 requestV2 = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789"
            };

            IyzipayResourceV2 response = await iyziLink.DeleteAsync(token, requestV2).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.AreEqual(Locale.TR.ToString(), response.Locale);
            Assert.AreEqual("123456789", response.ConversationId);
            Assert.NotNull(response.SystemTime);
        }
    }
}