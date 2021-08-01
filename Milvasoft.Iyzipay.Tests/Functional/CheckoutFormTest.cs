using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Tests.Functional.Builder;
using Milvasoft.Iyzipay.Tests.Functional.Builder.Request;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class CheckoutFormTest : BaseTest
    {
        [Test]
        public async Task Should_Initialize_Checkout_Form_For_Standard_Merchant()
        {
            IReadOnlyList<BasketItem> basketItems =
                new ReadOnlyCollection<BasketItem>(new List<BasketItem>()
                {
                    BasketItemBuilder.Create().Price("0.3").Build()
                });

            CreateCheckoutFormInitializeRequest request = CreateCheckoutFormInitializeRequestBuilder.Create()
                .Price("0.3")
                .PaymentGroup(PaymentGroup.LISTING.ToString())
                .PaidPrice("0.4")
                .CallbackUrl("https://www.merchant.com/callback")
                .BasketItems(basketItems)
                .Build();

            CheckoutFormInitialize checkoutFormInitialize = new(RestHttpClient);

            checkoutFormInitialize = await checkoutFormInitialize.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(request);

            Assert.AreEqual(Status.SUCCESS.ToString(), checkoutFormInitialize.Status);
            Assert.AreEqual(Locale.TR.ToString(), checkoutFormInitialize.Locale);
            Assert.NotNull(checkoutFormInitialize.SystemTime);
            Assert.NotNull(checkoutFormInitialize.Token);
            Assert.NotNull(checkoutFormInitialize.CheckoutFormContent);
        }

        [Test]
        public async Task Should_Retrieve_Checkout_Form_Result()
        {
            IReadOnlyList<BasketItem> basketItems =
                new ReadOnlyCollection<BasketItem>(new List<BasketItem>()
                {
                    BasketItemBuilder.Create().Price("0.3").Build()
                });

            CreateCheckoutFormInitializeRequest request = CreateCheckoutFormInitializeRequestBuilder.Create()
                .Price("0.3")
                .PaymentGroup(PaymentGroup.LISTING.ToString())
                .PaidPrice("0.4")
                .CallbackUrl("https://www.merchant.com/callback")
                .BasketItems(basketItems)
                .Build();

            CheckoutFormInitialize checkoutFormInitialize = new(RestHttpClient);

            checkoutFormInitialize = await checkoutFormInitialize.CreateAsync(request).ConfigureAwait(false);

            RetrieveCheckoutFormRequest retrieveCheckoutFormRequest = RetrieveCheckoutFormRequestBuilder.Create()
                .Token(checkoutFormInitialize.Token)
                .Build();

            CheckoutForm checkoutForm = new(RestHttpClient);

            checkoutForm = await checkoutForm.RetrieveAsync(retrieveCheckoutFormRequest).ConfigureAwait(false);

            PrintResponse(checkoutForm);

            Assert.NotNull(checkoutForm.ErrorMessage);
            Assert.AreEqual(Status.FAILURE.ToString(), checkoutForm.Status);
            Assert.NotNull(checkoutForm.SystemTime);
        }
    }
}
