using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Model.V2;
using Milvasoft.Iyzipay.Model.V2.Subscription;
using Milvasoft.Iyzipay.Request.V2.Subscription;
using Milvasoft.Iyzipay.Utils.Concrete;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class SubscriptionTest : BaseTest
    {

        [Test]
        public async Task Should_Initialize_CheckoutForm()
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

            ResponseData<ProductResource> createProductResponse = await product.CreateAsync(createProductRequest).ConfigureAwait(false);

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

            PlanResource planResource = (await plan.CreateAsync(createPlanRequest).ConfigureAwait(false)).Data;

            InitializeCheckoutFormRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                Customer = new CheckoutFormCustomer
                {
                    Email = $"iyzico-{randomString}@iyzico.com",
                    Name = "customer-name",
                    Surname = "customer-surname",
                    BillingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "billing-address-description",
                        ContactName = "billing-contact-name",
                        ZipCode = "010101"
                    },
                    ShippingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "shipping-address-description",
                        ContactName = "shipping-contact-name",
                        ZipCode = "010102"
                    },
                    GsmNumber = "+905350000000",
                    IdentityNumber = "55555555555",
                },
                CallbackUrl = "https://www.google.com",
                ConversationId = "123456789",
                PricingPlanReferenceCode = planResource.ReferenceCode,
                SubscriptionInitialStatus = SubscriptionStatus.PENDING.ToString()
            };

            Subscription subscription = new(RestHttpClientV2);

            CheckoutFormResource response = await subscription.InitializeCheckoutFormAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.IsNotNull(response.CheckoutFormContent);
            Assert.IsNotNull(response.Token);
            Assert.IsNotNull(response.TokenExpireTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Initialize_Subscription()
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

            ResponseData<ProductResource> createProductResponse = await product.CreateAsync(createProductRequest).ConfigureAwait(false);

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

            PlanResource planResource = (await plan.CreateAsync(createPlanRequest).ConfigureAwait(false)).Data;

            SubscriptionInitializeRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                Customer = new CheckoutFormCustomer
                {
                    Email = $"iyzico-{randomString}@iyzico.com",
                    Name = "customer-name",
                    Surname = "customer-surname",
                    BillingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "billing-address-description",
                        ContactName = "billing-contact-name",
                        ZipCode = "010101"
                    },
                    ShippingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "shipping-address-description",
                        ContactName = "shipping-contact-name",
                        ZipCode = "010102"
                    },

                    GsmNumber = "+905350000000",
                    IdentityNumber = "55555555555"
                },
                PaymentCard = new CardInfo
                {
                    CardNumber = "5528790000000008",
                    CardHolderName = "iyzico",
                    ExpireMonth = "12",
                    ExpireYear = "2029",
                    Cvc = "123",
                    RegisterConsumerCard = true
                },
                ConversationId = "123456789",
                PricingPlanReferenceCode = planResource.ReferenceCode
            };

            Subscription subscription = new(RestHttpClientV2);

            ResponseData<SubscriptionCreatedResource> response = await subscription.InitializeAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
            Assert.NotNull(response.Data.ReferenceCode);
            Assert.NotNull(response.Data.ParentReferenceCode);
            Assert.AreEqual(planResource.ReferenceCode, response.Data.PricingPlanReferenceCode);
            Assert.AreEqual(SubscriptionStatus.ACTIVE.ToString(), response.Data.SubscriptionStatus);
            Assert.AreEqual(3, response.Data.TrialDays);
            Assert.NotNull(response.Data.TrialStartDate);
            Assert.NotNull(response.Data.TrialEndDate);
            Assert.NotNull(response.Data.StartDate);

        }

        [Test]
        public async Task Should_Get_CheckoutForm_With_Token()
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

            ResponseData<ProductResource> createProductResponse = await product.CreateAsync(createProductRequest).ConfigureAwait(false);

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

            PlanResource planResource = (await plan.CreateAsync(createPlanRequest).ConfigureAwait(false)).Data;

            InitializeCheckoutFormRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                Customer = new CheckoutFormCustomer
                {
                    Email = $"iyzico-{randomString}@iyzico.com",
                    Name = "customer-name",
                    Surname = "customer-surname",
                    BillingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "billing-address-description",
                        ContactName = "billing-contact-name",
                        ZipCode = "010101"
                    },
                    ShippingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "shipping-address-description",
                        ContactName = "shipping-contact-name",
                        ZipCode = "010102"
                    },
                    GsmNumber = "+905350000000",
                    IdentityNumber = "55555555555",
                },
                CallbackUrl = "https://www.google.com",
                ConversationId = "123456789",
                PricingPlanReferenceCode = planResource.ReferenceCode,
                SubscriptionInitialStatus = SubscriptionStatus.PENDING.ToString()
            };

            Subscription subscription = new(RestHttpClientV2);

            CheckoutFormResource response = await subscription.InitializeCheckoutFormAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            RetrieveCheckoutFormResultRequest retrieveCheckoutFormResultRequest = new RetrieveCheckoutFormResultRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                Token = response.Token
            };

            var retrieveCheckoutFormResult = await subscription.RetrieveCheckoutFormResult(retrieveCheckoutFormResultRequest);

            Assert.AreEqual(retrieveCheckoutFormResult.Status, Status.FAILURE.ToString());
            Assert.AreEqual(retrieveCheckoutFormResult.ErrorMessage, "Ödeme formu tamamlanmamış.");
            Assert.AreEqual(retrieveCheckoutFormResult.StatusCode, 422);
        }

        [Test]
        public async Task Should_Activate_Subscription()
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

            ResponseData<ProductResource> createProductResponse = await product.CreateAsync(createProductRequest).ConfigureAwait(false);

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

            PlanResource planResource = (await plan.CreateAsync(createPlanRequest).ConfigureAwait(false)).Data;

            SubscriptionInitializeRequest subscriptionInitializeRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Customer = new CheckoutFormCustomer
                {
                    Email = $"iyzico-{randomString}@iyzico.com",
                    Name = "customer-name",
                    Surname = "customer-surname",
                    BillingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "billing-address-description",
                        ContactName = "billing-contact-name",
                        ZipCode = "010101"
                    },
                    ShippingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "shipping-address-description",
                        ContactName = "shipping-contact-name",
                        ZipCode = "010102"
                    },

                    GsmNumber = "+905350000000",
                    IdentityNumber = "55555555555"
                },
                PaymentCard = new CardInfo
                {
                    CardNumber = "5528790000000008",
                    CardHolderName = "iyzico",
                    ExpireMonth = "12",
                    ExpireYear = "2029",
                    Cvc = "123",
                    RegisterConsumerCard = true
                },
                ConversationId = "123456789",
                PricingPlanReferenceCode = planResource.ReferenceCode,
                SubscriptionInitialStatus = SubscriptionStatus.PENDING.ToString()
            };

            Subscription subscription = new(RestHttpClientV2);

            ResponseData<SubscriptionCreatedResource> initializeResponse = await subscription.InitializeAsync(subscriptionInitializeRequest).ConfigureAwait(false);

            ActivateSubscriptionRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubscriptionReferenceCode = initializeResponse.Data.ReferenceCode
            };

            IyzipayResourceV2 response = await subscription.ActivateAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Ignore("Test needs failed payment (OrderStatus=Failed,SubscriptionStatus=Unpaid), but we can not supply this condition in test now.")]
        public async Task Should_Retry_Subscription()
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

            ResponseData<ProductResource> createProductResponse = await product.CreateAsync(createProductRequest).ConfigureAwait(false);

            CreatePlanRequest createPlanRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Name = $"plan-name-{randomString}",
                ConversationId = "123456789",
                Price = "5.23",
                CurrencyCode = Currency.TRY.ToString(),
                PaymentInterval = PaymentInterval.WEEKLY.ToString(),
                RecurrenceCount = 12,
                PaymentIntervalCount = 1,
                PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
                ProductReferenceCode = createProductResponse.Data.ReferenceCode
            };

            Plan plan = new(RestHttpClientV2);

            PlanResource planResource = (await plan.CreateAsync(createPlanRequest).ConfigureAwait(false)).Data;

            SubscriptionInitializeRequest subscriptionInitializeRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Customer = new CheckoutFormCustomer
                {
                    Email = $"iyzico-{randomString}@iyzico.com",
                    Name = "customer-name",
                    Surname = "customer-surname",
                    BillingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "billing-address-description",
                        ContactName = "billing-contact-name",
                        ZipCode = "010101"
                    },
                    ShippingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "shipping-address-description",
                        ContactName = "shipping-contact-name",
                        ZipCode = "010102"
                    },

                    GsmNumber = "+905350000000",
                    IdentityNumber = "55555555555"
                },
                PaymentCard = new CardInfo
                {
                    CardNumber = "5528790000000008",
                    CardHolderName = "iyzico",
                    ExpireMonth = "12",
                    ExpireYear = "2029",
                    Cvc = "123"
                },
                ConversationId = "123456789",
                PricingPlanReferenceCode = planResource.ReferenceCode,
                SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString()
            };

            Subscription subscription = new(RestHttpClientV2);

            ResponseData<SubscriptionCreatedResource> initializeResponse = await subscription.InitializeAsync(subscriptionInitializeRequest).ConfigureAwait(false);

            RetrieveSubscriptionRequest retrieveSubscriptionRequest = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubscriptionReferenceCode = initializeResponse.Data.ReferenceCode
            };
            ResponseData<SubscriptionResource> subscriptionResponse = await subscription.RetrieveAsync(retrieveSubscriptionRequest).ConfigureAwait(false);

            RetrySubscriptionRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubscriptionOrderReferenceCode = subscriptionResponse.Data.SubscriptionOrders.FirstOrDefault()?.ReferenceCode
            };

            IyzipayResourceV2 response = await subscription.RetryAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Upgrade_Subscription()
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

            ResponseData<ProductResource> createProductResponse = await product.CreateAsync(createProductRequest).ConfigureAwait(false);

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

            PlanResource planResource = (await plan.CreateAsync(createPlanRequest).ConfigureAwait(false)).Data;

            SubscriptionInitializeRequest subscriptionInitializeRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Customer = new CheckoutFormCustomer
                {
                    Email = $"iyzico-{randomString}@iyzico.com",
                    Name = "customer-name",
                    Surname = "customer-surname",
                    BillingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "billing-address-description",
                        ContactName = "billing-contact-name",
                        ZipCode = "010101"
                    },
                    ShippingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "shipping-address-description",
                        ContactName = "shipping-contact-name",
                        ZipCode = "010102"
                    },

                    GsmNumber = "+905350000000",
                    IdentityNumber = "55555555555"
                },
                PaymentCard = new CardInfo
                {
                    CardNumber = "5528790000000008",
                    CardHolderName = "iyzico",
                    ExpireMonth = "12",
                    ExpireYear = "2029",
                    Cvc = "123",
                    RegisterConsumerCard = true
                },
                ConversationId = "123456789",
                PricingPlanReferenceCode = planResource.ReferenceCode,
                SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString()
            };

            Subscription subscription = new(RestHttpClientV2);

            ResponseData<SubscriptionCreatedResource> initializeResponse = await subscription.InitializeAsync(subscriptionInitializeRequest).ConfigureAwait(false);

            CreatePlanRequest newPlanRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Name = $"new-plan-name-{randomString}",
                ConversationId = "123456789",
                TrialPeriodDays = 2,
                Price = "3.23",
                CurrencyCode = Currency.TRY.ToString(),
                PaymentInterval = PaymentInterval.WEEKLY.ToString(),
                RecurrenceCount = 2,
                PaymentIntervalCount = 1,
                PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
                ProductReferenceCode = createProductResponse.Data.ReferenceCode
            };

            PlanResource newPlanResource = (await plan.CreateAsync(newPlanRequest).ConfigureAwait(false)).Data;

            UpgradeSubscriptionRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubscriptionReferenceCode = initializeResponse.Data.ReferenceCode,
                NewPricingPlanReferenceCode = newPlanResource.ReferenceCode,
                UseTrial = true,
                ResetRecurrenceCount = true,
                UpgradePeriod = SubscriptionUpgradePeriod.NOW.ToString()
            };

            IyzipayResourceV2 response = await subscription.UpgradeAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Cancel_Subscription()
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

            ResponseData<ProductResource> createProductResponse = await product.CreateAsync(createProductRequest).ConfigureAwait(false);

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

            PlanResource planResource = (await plan.CreateAsync(createPlanRequest).ConfigureAwait(false)).Data;

            SubscriptionInitializeRequest subscriptionInitializeRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Customer = new CheckoutFormCustomer
                {
                    Email = $"iyzico-{randomString}@iyzico.com",
                    Name = "customer-name",
                    Surname = "customer-surname",
                    BillingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "billing-address-description",
                        ContactName = "billing-contact-name",
                        ZipCode = "010101"
                    },
                    ShippingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "shipping-address-description",
                        ContactName = "shipping-contact-name",
                        ZipCode = "010102"
                    },

                    GsmNumber = "+905350000000",
                    IdentityNumber = "55555555555"
                },
                PaymentCard = new CardInfo
                {
                    CardNumber = "5528790000000008",
                    CardHolderName = "iyzico",
                    ExpireMonth = "12",
                    ExpireYear = "2029",
                    Cvc = "123",
                    RegisterConsumerCard = true
                },
                ConversationId = "123456789",
                PricingPlanReferenceCode = planResource.ReferenceCode,
                SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString()
            };

            Subscription subscription = new(RestHttpClientV2);

            ResponseData<SubscriptionCreatedResource> initializeResponse = await subscription.InitializeAsync(subscriptionInitializeRequest).ConfigureAwait(false);

            CancelSubscriptionRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubscriptionReferenceCode = initializeResponse.Data.ReferenceCode
            };

            IyzipayResourceV2 response = await subscription.CancelAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Retrieve_Subscription()
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

            ResponseData<ProductResource> createProductResponse = await product.CreateAsync(createProductRequest).ConfigureAwait(false);

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

            PlanResource planResource = (await plan.CreateAsync(createPlanRequest).ConfigureAwait(false)).Data;

            SubscriptionInitializeRequest subscriptionInitializeRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Customer = new CheckoutFormCustomer
                {
                    Email = $"iyzico-{randomString}@iyzico.com",
                    Name = "customer-name",
                    Surname = "customer-surname",
                    BillingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "billing-address-description",
                        ContactName = "billing-contact-name",
                        ZipCode = "010101"
                    },
                    ShippingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "shipping-address-description",
                        ContactName = "shipping-contact-name",
                        ZipCode = "010102"
                    },

                    GsmNumber = "+905350000000",
                    IdentityNumber = "55555555555"
                },
                PaymentCard = new CardInfo
                {
                    CardNumber = "5528790000000008",
                    CardHolderName = "iyzico",
                    ExpireMonth = "12",
                    ExpireYear = "2029",
                    Cvc = "123",
                    RegisterConsumerCard = true
                },
                ConversationId = "123456789",
                PricingPlanReferenceCode = planResource.ReferenceCode,
                SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString()
            };

            Subscription subscription = new(RestHttpClientV2);

            ResponseData<SubscriptionCreatedResource> initializeResponse = await subscription.InitializeAsync(subscriptionInitializeRequest).ConfigureAwait(false);

            RetrieveSubscriptionRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubscriptionReferenceCode = initializeResponse.Data.ReferenceCode
            };

            ResponseData<SubscriptionResource> response = await subscription.RetrieveAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
            Assert.NotNull(response.Data.ReferenceCode);
            Assert.NotNull(response.Data.ParentReferenceCode);
            Assert.AreEqual(planResource.ReferenceCode, response.Data.PricingPlanReferenceCode);
            Assert.AreEqual(SubscriptionStatus.ACTIVE.ToString(), response.Data.SubscriptionStatus);
            Assert.AreEqual($"iyzico-{randomString}@iyzico.com", response.Data.CustomerEmail);
            Assert.AreEqual(3, response.Data.TrialDays);
            Assert.NotNull(response.Data.TrialStartDate);
            Assert.NotNull(response.Data.TrialEndDate);
            Assert.NotNull(response.Data.StartDate);
        }

        [Test]
        public async Task Should_Search_Subscription()
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

            ResponseData<ProductResource> createProductResponse = await product.CreateAsync(createProductRequest).ConfigureAwait(false);

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

            PlanResource planResource = (await plan.CreateAsync(createPlanRequest).ConfigureAwait(false)).Data;

            SubscriptionInitializeRequest subscriptionInitializeRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Customer = new CheckoutFormCustomer
                {
                    Email = $"iyzico-{randomString}@iyzico.com",
                    Name = "customer-name",
                    Surname = "customer-surname",
                    BillingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "billing-address-description",
                        ContactName = "billing-contact-name",
                        ZipCode = "010101"
                    },
                    ShippingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "shipping-address-description",
                        ContactName = "shipping-contact-name",
                        ZipCode = "010102"
                    },

                    GsmNumber = "+905350000000",
                    IdentityNumber = "55555555555"
                },
                PaymentCard = new CardInfo
                {
                    CardNumber = "5528790000000008",
                    CardHolderName = "iyzico",
                    ExpireMonth = "12",
                    ExpireYear = "2029",
                    Cvc = "123",
                    RegisterConsumerCard = true
                },
                ConversationId = "123456789",
                PricingPlanReferenceCode = planResource.ReferenceCode,
                SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString()
            };

            Subscription subscription = new(RestHttpClientV2);

            ResponseData<SubscriptionCreatedResource> initializeResponse = await subscription.InitializeAsync(subscriptionInitializeRequest).ConfigureAwait(false);

            SearchSubscriptionRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubscriptionReferenceCode = initializeResponse.Data.ReferenceCode,
                Page = 1,
                Count = 1,
                SubscriptionStatus = SubscriptionStatus.ACTIVE.ToString(),
                PricingPlanReferenceCode = planResource.ReferenceCode
            };

            ResponsePagingData<SubscriptionResource> response = await subscription.SearchAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.AreEqual(1, response.Data.Items.Count);
            Assert.AreEqual(1, response.Data.CurrentPage);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
            Assert.NotNull(response.Data.Items.First().ReferenceCode);
            Assert.NotNull(response.Data.Items.First().ParentReferenceCode);
            Assert.AreEqual(planResource.ReferenceCode, response.Data.Items.First().PricingPlanReferenceCode);
            Assert.AreEqual(SubscriptionStatus.ACTIVE.ToString(), response.Data.Items.First().SubscriptionStatus);
            Assert.AreEqual($"iyzico-{randomString}@iyzico.com", response.Data.Items.First().CustomerEmail);
            Assert.AreEqual(3, response.Data.Items.First().TrialDays);
            Assert.NotNull(response.Data.Items.First().TrialStartDate);
            Assert.NotNull(response.Data.Items.First().TrialEndDate);
            Assert.NotNull(response.Data.Items.First().StartDate);
        }

        [Test]
        public async Task Should_Update_Subscription_Card()
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

            ResponseData<ProductResource> createProductResponse = await product.CreateAsync(createProductRequest).ConfigureAwait(false);

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

            PlanResource planResource = (await plan.CreateAsync(createPlanRequest).ConfigureAwait(false)).Data;

            SubscriptionInitializeRequest subscriptionInitializeRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Customer = new CheckoutFormCustomer
                {
                    Email = $"iyzico-{randomString}@iyzico.com",
                    Name = "customer-name",
                    Surname = "customer-surname",
                    BillingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "billing-address-description",
                        ContactName = "billing-contact-name",
                        ZipCode = "010101"
                    },
                    ShippingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "shipping-address-description",
                        ContactName = "shipping-contact-name",
                        ZipCode = "010102"
                    },

                    GsmNumber = "+905350000000",
                    IdentityNumber = "55555555555"
                },
                PaymentCard = new CardInfo
                {
                    CardNumber = "5528790000000008",
                    CardHolderName = "iyzico",
                    ExpireMonth = "12",
                    ExpireYear = "2029",
                    Cvc = "123",
                    RegisterConsumerCard = true
                },
                ConversationId = "123456789",
                PricingPlanReferenceCode = planResource.ReferenceCode,
                SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString()
            };

            Subscription subscription = new(RestHttpClientV2);

            ResponseData<SubscriptionCreatedResource> initializeResponse = await subscription.InitializeAsync(subscriptionInitializeRequest).ConfigureAwait(false);

            UpdateCardRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                CustomerReferenceCode = initializeResponse.Data.CustomerReferenceCode,
                CallbackUrl = "https://www.google.com"
            };

            UpdateCardFormResource response = await subscription.UpdateCardAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
            Assert.NotNull(response.CheckoutFormContent);
            Assert.NotNull(response.Token);
            Assert.NotNull(response.TokenExpireTime);
        }
    }
}