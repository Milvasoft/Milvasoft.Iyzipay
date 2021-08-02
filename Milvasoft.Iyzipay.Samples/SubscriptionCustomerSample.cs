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
    public class SubscriptionCustomerSample : Sample
    {
        [Test]
        public async Task Should_Create_Customer()
        {
            string randomString = $"{DateTime.Now:yyyyMMddHHmmssfff}";

            CreateCustomerRequest createCustomerRequest = new()
            {
                Email = $"iyzico-{randomString}@iyzico.com",
                Locale = Locale.TR.ToString(),
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
                ConversationId = "123456789",
                GsmNumber = "+905350000000",
                IdentityNumber = "55555555555"
            };

            var subMerchant = new Customer(RestHttpClientV2);

            ResponseData<CustomerResource> response = await subMerchant.CreateAsync(createCustomerRequest).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.AreEqual($"iyzico-{randomString}@iyzico.com", response.Data.Email);
            Assert.AreEqual("customer-name", response.Data.Name);
            Assert.AreEqual("customer-surname", response.Data.Surname);
            Assert.AreEqual("+905350000000", response.Data.GsmNumber);
            Assert.AreEqual("55555555555", response.Data.IdentityNumber);
            Assert.AreEqual("İstanbul", response.Data.BillingAddress.City);
            Assert.AreEqual("Türkiye", response.Data.BillingAddress.Country);
            Assert.AreEqual("billing-address-description", response.Data.BillingAddress.Description);
            Assert.AreEqual("billing-contact-name", response.Data.BillingAddress.ContactName);
            Assert.AreEqual("010101", response.Data.BillingAddress.ZipCode);
            Assert.AreEqual("İstanbul", response.Data.ShippingAddress.City);
            Assert.AreEqual("Türkiye", response.Data.ShippingAddress.Country);
            Assert.AreEqual("shipping-address-description", response.Data.ShippingAddress.Description);
            Assert.AreEqual("shipping-contact-name", response.Data.ShippingAddress.ContactName);
            Assert.AreEqual("010102", response.Data.ShippingAddress.ZipCode);
            Assert.IsNotNull(response.Data.ReferenceCode);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Update_Customer()
        {
            string randomString = $"{DateTime.Now:yyyyMMddHHmmssfff}";

            UpdateCustomerRequest updateCustomerRequest = new()
            {
                Locale = Locale.TR.ToString(),
                Name = "upd-customer-name",
                Surname = "upd-customer-surname",
                BillingAddress = new Address
                {
                    City = "upd-İstanbul",
                    Country = "upd-Türkiye",
                    Description = "upd-billing-address-description",
                    ContactName = "upd-billing-contact-name",
                    ZipCode = "010101"
                },
                ShippingAddress = new Address
                {
                    City = "upd-İstanbul",
                    Country = "upd-Türkiye",
                    Description = "upd-shipping-address-description",
                    ContactName = "upd-shipping-contact-name",
                    ZipCode = "010102"
                },
                ConversationId = "123456789",
                IdentityNumber = "55555555551",
                CustomerReferenceCode = "customerReferenceCode"
            };

            var subMerchant = new Customer(RestHttpClientV2);

            ResponseData<CustomerResource> response = await subMerchant.UpdateAsync(updateCustomerRequest).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(Status.SUCCESS.ToString(), response.Status);
            Assert.AreEqual($"iyzico-{randomString}@iyzico.com", response.Data.Email);
            Assert.AreEqual("upd-customer-name", response.Data.Name);
            Assert.AreEqual("upd-customer-surname", response.Data.Surname);
            Assert.AreEqual("+905350000000", response.Data.GsmNumber);
            Assert.AreEqual("55555555551", response.Data.IdentityNumber);
            Assert.AreEqual("upd-İstanbul", response.Data.BillingAddress.City);
            Assert.AreEqual("upd-Türkiye", response.Data.BillingAddress.Country);
            Assert.AreEqual("upd-billing-address-description", response.Data.BillingAddress.Description);
            Assert.AreEqual("upd-billing-contact-name", response.Data.BillingAddress.ContactName);
            Assert.AreEqual("010101", response.Data.BillingAddress.ZipCode);
            Assert.AreEqual("upd-İstanbul", response.Data.ShippingAddress.City);
            Assert.AreEqual("upd-Türkiye", response.Data.ShippingAddress.Country);
            Assert.AreEqual("upd-shipping-address-description", response.Data.ShippingAddress.Description);
            Assert.AreEqual("upd-shipping-contact-name", response.Data.ShippingAddress.ContactName);
            Assert.AreEqual("010102", response.Data.ShippingAddress.ZipCode);
            Assert.IsNotNull(response.Data.ReferenceCode);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_Retrieve_Customer()
        {
            RetrieveCustomerRequest retrieveCustomerRequest = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                CustomerReferenceCode = "customerReferenceCode"
            };

            var subMerchant = new Customer(RestHttpClientV2);

            ResponseData<CustomerResource> response = await subMerchant.RetrieveAsync(retrieveCustomerRequest).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual("customerReferenceCode", response?.Data?.ReferenceCode);
            Assert.IsNotNull(response.Data.ReferenceCode);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }

        [Test]
        public async Task Should_RetrieveAll_Customer()
        {
            PagingRequest pagingRequest = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                Page = 1,
                Count = 1
            };

            var subMerchant = new Customer(RestHttpClientV2);

            ResponsePagingData<CustomerResource> response = await subMerchant.RetrieveAllAsync(pagingRequest).ConfigureAwait(false);

            PrintResponse(response);

            Assert.AreEqual(response.Status, Status.SUCCESS.ToString());
            Assert.AreEqual(1, response.Data.Items.Count);
            Assert.AreEqual(1, response.Data.CurrentPage);
            Assert.IsNotNull(response.SystemTime);
            Assert.Null(response.ErrorMessage);
        }
    }
}