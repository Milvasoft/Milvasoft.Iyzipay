using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Model.V2;
using Milvasoft.Iyzipay.Model.V2.Subscription;
using Milvasoft.Iyzipay.Request.V2.Subscription;
using Milvasoft.Iyzipay.Utils.Concrete;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class SubscriptionProductSample : Sample
    {
        [Test]
        public async Task Should_Create_Product()
        {
            string randomString = $"{DateTime.Now:yyyyMMddHHmmssfff}";
            CreateProductRequest request = new()
            {
                Description = "product-description",
                Locale = Locale.TR.ToString(),
                Name = $"product-name-{randomString}",
                ConversationId = "123456789"
            };

            var product = new Product(RestHttpClientV2);

            ResponseData<ProductResource> response = await product.Create(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.AreEqual($"product-name-{randomString}", response.Data.Name);
            Assert.AreEqual("product-description", response.Data.Description);
            Assert.IsNotNull(response.Data.ReferenceCode);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Update_Product()
        {
            string randomString = $"{DateTime.Now:yyyyMMddHHmmssfff}";

            UpdateProductRequest updateProductRequest = new()
            {
                Description = "updated-description",
                Locale = Locale.TR.ToString(),
                Name = $"updated-product-name-{randomString}",
                ConversationId = "123456789",
                ProductReferenceCode = "productReferenceCode"
            };

            var product = new Product(RestHttpClientV2);
            ResponseData<ProductResource> response = await product.Update(updateProductRequest).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
            Assert.AreEqual($"updated-product-name-{randomString}", response.Data.Name);
            Assert.AreEqual("updated-description", response.Data.Description);
            Assert.AreEqual("productReferenceCode", response.Data.ReferenceCode);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Delete_Product()
        {
            DeleteProductRequest updateProductRequest = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                ProductReferenceCode = "productReferenceCode"
            };

            var product = new Product(RestHttpClientV2);

            IyzipayResourceV2 response = await product.Delete(updateProductRequest).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Retrieve_Product()
        {
            string randomString = $"{DateTime.Now:yyyyMMddHHmmssfff}";

            RetrieveProductRequest retrieveProductRequest = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                ProductReferenceCode = "productReferenceCode"
            };

            var product = new Product(RestHttpClientV2);

            ResponseData<ProductResource> response = await product.Retrieve(retrieveProductRequest).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
            Assert.AreEqual("productReferenceCode", response.Data.ReferenceCode);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_RetrieveAll_Product()
        {
            PagingRequest pagingRequest = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                Page = 1,
                Count = 1
            };

            var product = new Product(RestHttpClientV2);

            ResponsePagingData<ProductResource> response = await product.RetrieveAll(pagingRequest).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
            Assert.AreEqual(1, response.Data.Items.Count);
            Assert.AreEqual(1, response.Data.CurrentPage);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }
    }
}