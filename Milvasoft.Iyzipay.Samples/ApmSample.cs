using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class ApmSample : Sample
    {
        [Test]
        public async Task Should_Initialize_Apm_Payment()
        {
            CreateApmInitializeRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                Price = "1",
                PaidPrice = "1.2",
                Currency = Currency.EUR.ToString(),
                CountryCode = "DE",
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                AccountHolderName = "success",
                MerchantCallbackUrl = "https://www.merchant.com/callback",
                MerchantErrorUrl = "https://www.merchant.com/error",
                MerchantNotificationUrl = "https://www.merchant.com/notification",
                ApmType = ApmType.SOFORT.ToString()
            };

            Buyer buyer = new()
            {
                Id = "BY789",
                Name = "John",
                Surname = "Doe",
                GsmNumber = "+905350000000",
                Email = "email@email.com",
                IdentityNumber = "74300864791",
                LastLoginDate = "2015-10-05 12:43:35",
                RegistrationDate = "2013-04-21 15:12:09",
                RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                Ip = "85.34.78.112",
                City = "Istanbul",
                Country = "Turkey",
                ZipCode = "34732"
            };
            request.Buyer = buyer;

            Address shippingAddress = new()
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                ZipCode = "34742"
            };
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new()
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                ZipCode = "34742"
            };
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new();
            BasketItem firstBasketItem = new()
            {
                Id = "BI101",
                Name = "Binocular",
                Category1 = "Collectibles",
                Category2 = "Accessories",
                ItemType = BasketItemType.PHYSICAL.ToString(),
                Price = "0.3"
            };
            basketItems.Add(firstBasketItem);

            BasketItem secondBasketItem = new()
            {
                Id = "BI102",
                Name = "Game code",
                Category1 = "Game",
                Category2 = "Online Game Items",
                ItemType = BasketItemType.VIRTUAL.ToString(),
                Price = "0.5"
            };
            basketItems.Add(secondBasketItem);

            BasketItem thirdBasketItem = new()
            {
                Id = "BI103",
                Name = "Usb",
                Category1 = "Electronics",
                Category2 = "Usb / Cable",
                ItemType = BasketItemType.PHYSICAL.ToString(),
                Price = "0.2"
            };
            basketItems.Add(thirdBasketItem);
            request.BasketItems = basketItems;

            var apm = new Apm(RestHttpClient);

            apm = await apm.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(apm);

            Assert.AreEqual(Status.SUCCESS.ToString(), apm.Status);
            Assert.AreEqual(Locale.TR.ToString(), apm.Locale);
            Assert.AreEqual("123456789", apm.ConversationId);
            Assert.IsNotNull(apm.SystemTime);
            Assert.IsNull(apm.ErrorCode);
            Assert.IsNull(apm.ErrorMessage);
            Assert.IsNull(apm.ErrorGroup);
        }

        [Test]
        public async Task Should_Retrieve_Apm_Result()
        {
            RetrieveApmRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                PaymentId = "1"
            };

            var apm = new Apm(RestHttpClient);

            apm = await apm.RetrieveAsync(request).ConfigureAwait(false);

            PrintResponse(apm);

            Assert.AreEqual(Status.SUCCESS.ToString(), apm.Status);
            Assert.AreEqual(Locale.TR.ToString(), apm.Locale);
            Assert.AreEqual("123456789", apm.ConversationId);
        }
    }
}
