using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Model.V2;
using Milvasoft.Iyzipay.Model.V2.Subscription;
using Milvasoft.Iyzipay.Request.V2.Subscription;
using Milvasoft.Iyzipay.Utils.Concrete;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class SubscriptionSample : Sample
    {
        [Test]
        public async Task Should_Initialize_CheckoutForm()
        {
            string randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");

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
                PricingPlanReferenceCode = "pricingPlanReferenceCode",
                SubscriptionInitialStatus = SubscriptionStatus.PENDING.ToString()
            };

            var subscription = new Subscription(RestHttpClientV2);

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
                PricingPlanReferenceCode = "pricingPlanReferenceCode"
            };

            var subscription = new Subscription(RestHttpClientV2);

            ResponseData<SubscriptionCreatedResource> response = await subscription.InitializeAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
            Assert.NotNull(response.Data.ReferenceCode);
            Assert.NotNull(response.Data.ParentReferenceCode);
            Assert.AreEqual("pricingPlanReferenceCode", response.Data.PricingPlanReferenceCode);
            Assert.AreEqual(SubscriptionStatus.ACTIVE.ToString(), response.Data.SubscriptionStatus);
            Assert.AreEqual(3, response.Data.TrialDays);
            Assert.NotNull(response.Data.TrialStartDate);
            Assert.NotNull(response.Data.TrialEndDate);
            Assert.NotNull(response.Data.StartDate);
        }
        
        

        [Test]
        public async Task Should_Activate_Subscription()
        {
            ActivateSubscriptionRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubscriptionReferenceCode = "subscriptionReferenceCode"
            };

            var subscription = new Subscription(RestHttpClientV2);

            IyzipayResourceV2 response = await subscription.ActivateAsync(request);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Retry_Subscription()
        {
            RetrySubscriptionRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubscriptionOrderReferenceCode = "referenceCode"
            };

            var subscription = new Subscription(RestHttpClientV2);

            IyzipayResourceV2 response = await subscription.RetryAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Upgrade_Subscription()
        {
            UpgradeSubscriptionRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubscriptionReferenceCode = "subscriptionReferenceCode",
                NewPricingPlanReferenceCode = "newPricingPlanReferenceCode",
                UseTrial = true,
                ResetRecurrenceCount = true,
                UpgradePeriod = SubscriptionUpgradePeriod.NOW.ToString()
            };

            var subscription = new Subscription(RestHttpClientV2);

            IyzipayResourceV2 response = await subscription.UpgradeAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Cancel_Subscription()
        {
            CancelSubscriptionRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubscriptionReferenceCode = "subscriptionReferenceCode"
            };

            var subscription = new Subscription(RestHttpClientV2);

            IyzipayResourceV2 response = await subscription.CancelAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Retrieve_Subscription()
        {
            RetrieveSubscriptionRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubscriptionReferenceCode = "subscriptionReferenceCode"
            };

            var subscription = new Subscription(RestHttpClientV2);

            ResponseData<SubscriptionResource> response = await subscription.RetrieveAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
            Assert.AreEqual("subscriptionReferenceCode", response.Data.ReferenceCode);
        }

        [Test]
        public async Task Should_Search_Subscription()
        {
            SearchSubscriptionRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                SubscriptionReferenceCode = "subscriptionReferenceCode",
                Page = 1,
                Count = 1,
                SubscriptionStatus = SubscriptionStatus.ACTIVE.ToString(),
                PricingPlanReferenceCode = "pricingPlanReferenceCode"
            };

            var subscription = new Subscription(RestHttpClientV2);

            ResponsePagingData<SubscriptionResource> response = await subscription.SearchAsync(request).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.AreEqual(1, response.Data.Items.Count);
            Assert.AreEqual(1, response.Data.CurrentPage);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
            Assert.NotNull(response.Data.Items.First().ReferenceCode);
            Assert.NotNull(response.Data.Items.First().ParentReferenceCode);
            Assert.AreEqual("pricingPlanReferenceCode", response.Data.Items.First().PricingPlanReferenceCode);
            Assert.AreEqual(SubscriptionStatus.ACTIVE.ToString(), response.Data.Items.First().SubscriptionStatus);
        }

        [Test]
        public async Task Should_Update_Subscription_Card()
        {
            UpdateCardRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                CustomerReferenceCode = "customerReferenceCode",
                CallbackUrl = "https://www.google.com"
            };

            var subscription = new Subscription(RestHttpClientV2);

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