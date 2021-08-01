using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Samples
{
    public class ThreedsSample : Sample
    {
        [Test]
        public async Task Should_Initialize_Threeds()
        {
            CreatePaymentRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                Price = "1",
                PaidPrice = "1.2",
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                BasketId = "B67832",
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = "https://www.merchant.com/callback"
            };

            PaymentCard paymentCard = new()
            {
                CardHolderName = "John Doe",
                CardNumber = "5528790000000008",
                ExpireMonth = "12",
                ExpireYear = "2030",
                Cvc = "123",
                RegisterCard = 0
            };
            request.PaymentCard = paymentCard;

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

            var threedsInitialize = new ThreedsInitialize(RestHttpClient);

            threedsInitialize = await threedsInitialize.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(threedsInitialize);

            Assert.AreEqual(Status.SUCCESS.ToString(), threedsInitialize.Status);
            Assert.AreEqual(Locale.TR.ToString(), threedsInitialize.Locale);
            Assert.AreEqual("123456789", threedsInitialize.ConversationId);
            Assert.IsNotNull(threedsInitialize.SystemTime);
            Assert.IsNull(threedsInitialize.ErrorCode);
            Assert.IsNull(threedsInitialize.ErrorMessage);
            Assert.IsNull(threedsInitialize.ErrorGroup);
            Assert.IsNotNull(threedsInitialize.HtmlContent);
        }

        [Test]
        public async Task Should_Create_Threeds_Payment()
        {
            CreateThreedsPaymentRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                PaymentId = "1",
                ConversationData = "conversation data"
            };

            var threedsPayment = new ThreedsPayment(RestHttpClient);

            threedsPayment = await threedsPayment.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(threedsPayment);

            Assert.AreEqual(Status.SUCCESS.ToString(), threedsPayment.Status);
            Assert.AreEqual(Locale.TR.ToString(), threedsPayment.Locale);
            Assert.AreEqual("123456789", threedsPayment.ConversationId);
            Assert.IsNotNull(threedsPayment.SystemTime);
            Assert.IsNull(threedsPayment.ErrorCode);
            Assert.IsNull(threedsPayment.ErrorMessage);
            Assert.IsNull(threedsPayment.ErrorGroup);
        }
    }
}
