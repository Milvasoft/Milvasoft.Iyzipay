using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Model.V2;
using Milvasoft.Iyzipay.Model.V2.Subscription;
using Milvasoft.Iyzipay.Request.V2.Subscription;
using Milvasoft.Iyzipay.Tests.Functional.Util;
using Milvasoft.Iyzipay.Utils.Concrete;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class SubscriptionPlanTest : BaseTest
    {
        [Test]
        public async Task Should_Create_Plan()
        {
            string randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            CreateProductRequest createProductRequest = new()
            {
                Description = "product-description",
                Locale = Locale.TR.ToString(),
                Name = $"product-name-{randomString}",
                ConversationId = "123456789"
            };

            Product product = new(RestHttpClientV2);

            ResponseData<ProductResource> createProductResponse = await product.Create(createProductRequest).ConfigureAwait(false);

            CreatePlanRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                Name = $"plan-name-{randomString}",
                ConversationId = "123456789",
                TrialPeriodDays = 3,
                Price = "5.23",
                CurrencyCode = Currency.TRY.ToString(),
                PaymentInterval = PaymentInterval.WEEKLY.ToString(),
                RecurrenceCount = 12,
                PaymentIntervalCount = 1,
                PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
                ProductReferenceCode = createProductResponse.Data.ReferenceCode
            };

            Plan plan = new(RestHttpClientV2);

            ResponseData<PlanResource> response = await plan.Create(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
            Assert.AreEqual($"plan-name-{randomString}", response.Data.Name);
            Assert.AreEqual("5.23", response.Data.Price.RemoveTrailingZeros());
            Assert.AreEqual(Currency.TRY.ToString(), response.Data.CurrencyCode);
            Assert.AreEqual(createProductResponse.Data.ReferenceCode, response.Data.ProductReferenceCode);
            Assert.AreEqual(PaymentInterval.WEEKLY.ToString(), response.Data.PaymentInterval);
            Assert.AreEqual(1, response.Data.PaymentIntervalCount);
            Assert.AreEqual(3, response.Data.TrialPeriodDays);
            Assert.AreEqual(PlanPaymentType.RECURRING.ToString(), response.Data.PlanPaymentType);
            Assert.AreEqual(12, response.Data.RecurrenceCount);
            Assert.AreEqual("ACTIVE", response.Data.Status);
            Assert.IsNotNull(response.Data.ReferenceCode);
            Assert.IsNotNull(response.Data.CreatedDate);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Update_Plan()
        {
            string randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            CreateProductRequest createProductRequest = new()
            {
                Description = "product-description",
                Locale = Locale.TR.ToString(),
                Name = $"product-name-{randomString}",
                ConversationId = "123456789"
            };

            Product product = new(RestHttpClientV2);

            ResponseData<ProductResource> createProductResponse = await product.Create(createProductRequest).ConfigureAwait(false);

            CreatePlanRequest createPlanRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Name = $"plan-name-{randomString}",
                ConversationId = "123456789",
                TrialPeriodDays = 3,
                Price = "5.23",
                CurrencyCode = Currency.TRY.ToString(),
                PaymentInterval = PaymentInterval.WEEKLY.ToString(),
                RecurrenceCount = 12,
                PaymentIntervalCount = 1,
                PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
                ProductReferenceCode = createProductResponse.Data.ReferenceCode
            };

            Plan plan = new(RestHttpClientV2);

            ResponseData<PlanResource> createPlanResponse = await plan.Create(createPlanRequest).ConfigureAwait(false);

            UpdatePlanRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                Name = $"updated-plan-name-{randomString}",
                ConversationId = "123456789",
                TrialPeriodDays = 5,
                PricingPlanReferenceCode = createPlanResponse.Data.ReferenceCode
            };

            ResponseData<PlanResource> response = await plan.Update(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
            Assert.AreEqual($"updated-plan-name-{randomString}", response.Data.Name);
            Assert.AreEqual("5.23", response.Data.Price.RemoveTrailingZeros());
            Assert.AreEqual(Currency.TRY.ToString(), response.Data.CurrencyCode);
            Assert.AreEqual(createProductResponse.Data.ReferenceCode, response.Data.ProductReferenceCode);
            Assert.AreEqual(PaymentInterval.WEEKLY.ToString(), response.Data.PaymentInterval);
            Assert.AreEqual(1, response.Data.PaymentIntervalCount);
            Assert.AreEqual(5, response.Data.TrialPeriodDays);
            Assert.AreEqual(PlanPaymentType.RECURRING.ToString(), response.Data.PlanPaymentType);
            Assert.AreEqual(12, response.Data.RecurrenceCount);
            Assert.AreEqual("ACTIVE", response.Data.Status);
            Assert.IsNotNull(response.Data.ReferenceCode);
            Assert.IsNotNull(response.Data.CreatedDate);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Delete_Plan()
        {
            string randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            CreateProductRequest createProductRequest = new()
            {
                Description = "product-description",
                Locale = Locale.TR.ToString(),
                Name = $"product-name-{randomString}",
                ConversationId = "123456789"
            };

            Product product = new(RestHttpClientV2);

            ResponseData<ProductResource> createProductResponse = await product.Create(createProductRequest).ConfigureAwait(false);

            CreatePlanRequest createPlanRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Name = $"plan-name-{randomString}",
                ConversationId = "123456789",
                TrialPeriodDays = 3,
                Price = "5.23",
                CurrencyCode = Currency.TRY.ToString(),
                PaymentInterval = PaymentInterval.WEEKLY.ToString(),
                RecurrenceCount = 12,
                PaymentIntervalCount = 1,
                PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
                ProductReferenceCode = createProductResponse.Data.ReferenceCode
            };

            Plan plan = new(RestHttpClientV2);

            ResponseData<PlanResource> createPlanResponse = await plan.Create(createPlanRequest).ConfigureAwait(false);

            DeletePlanRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                PricingPlanReferenceCode = createPlanResponse.Data.ReferenceCode
            };

            IyzipayResourceV2 response = await plan.Delete(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Retrieve_Plan()
        {
            string randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            CreateProductRequest createProductRequest = new()
            {
                Description = "product-description",
                Locale = Locale.TR.ToString(),
                Name = $"product-name-{randomString}",
                ConversationId = "123456789"
            };

            Product product = new(RestHttpClientV2);

            ResponseData<ProductResource> createProductResponse = await product.Create(createProductRequest).ConfigureAwait(false);

            CreatePlanRequest createPlanRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Name = $"plan-name-{randomString}",
                ConversationId = "123456789",
                TrialPeriodDays = 3,
                Price = "5.23",
                CurrencyCode = Currency.TRY.ToString(),
                PaymentInterval = PaymentInterval.WEEKLY.ToString(),
                RecurrenceCount = 12,
                PaymentIntervalCount = 1,
                PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
                ProductReferenceCode = createProductResponse.Data.ReferenceCode
            };

            Plan plan = new(RestHttpClientV2);

            ResponseData<PlanResource> createPlanResponse = await plan.Create(createPlanRequest).ConfigureAwait(false);

            RetrievePlanRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                PricingPlanReferenceCode = createPlanResponse.Data.ReferenceCode
            };

            ResponseData<PlanResource> response = await plan.Retrieve(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
            Assert.AreEqual($"plan-name-{randomString}", response.Data.Name);
            Assert.AreEqual("5.23", response.Data.Price.RemoveTrailingZeros());
            Assert.AreEqual(Currency.TRY.ToString(), response.Data.CurrencyCode);
            Assert.AreEqual(createProductResponse.Data.ReferenceCode, response.Data.ProductReferenceCode);
            Assert.AreEqual(PaymentInterval.WEEKLY.ToString(), response.Data.PaymentInterval);
            Assert.AreEqual(1, response.Data.PaymentIntervalCount);
            Assert.AreEqual(3, response.Data.TrialPeriodDays);
            Assert.AreEqual(PlanPaymentType.RECURRING.ToString(), response.Data.PlanPaymentType);
            Assert.AreEqual(12, response.Data.RecurrenceCount);
            Assert.AreEqual("ACTIVE", response.Data.Status);
            Assert.IsNotNull(response.Data.ReferenceCode);
            Assert.IsNotNull(response.Data.CreatedDate);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_RetrieveAll_Plan()
        {
            string randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            CreateProductRequest createProductRequest = new()
            {
                Description = "product-description",
                Locale = Locale.TR.ToString(),
                Name = $"product-name-{randomString}",
                ConversationId = "123456789"
            };

            Product product = new(RestHttpClientV2);

            ResponseData<ProductResource> createProductResponse = await product.Create(createProductRequest).ConfigureAwait(false);

            CreatePlanRequest createPlanRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Name = $"plan-name-{randomString}",
                ConversationId = "123456789",
                TrialPeriodDays = 3,
                Price = "5.23",
                CurrencyCode = Currency.TRY.ToString(),
                PaymentInterval = PaymentInterval.WEEKLY.ToString(),
                RecurrenceCount = 12,
                PaymentIntervalCount = 1,
                PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
                ProductReferenceCode = createProductResponse.Data.ReferenceCode
            };

            Plan plan = new(RestHttpClientV2);

            await plan.Create(createPlanRequest).ConfigureAwait(false);

            RetrieveAllPlanRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                ProductReferenceCode = createProductResponse.Data.ReferenceCode,
                Count = 1,
                Page = 1
            };

            ResponsePagingData<PlanResource> response = await plan.RetrieveAll(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
            Assert.AreEqual(1, response.Data.Items.Count);
            Assert.AreEqual(1, response.Data.CurrentPage);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }
    }
}