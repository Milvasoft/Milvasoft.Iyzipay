using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Tests.Functional.Builder;
using Milvasoft.Iyzipay.Tests.Functional.Builder.Request;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class IyziupFormTest : BaseTest
    {
        [Test]
        public void Should_Initialize_Iyziup_Form_For_Standard_Merchant()
        {
            List<OrderItem> orderItems =
                new(new List<OrderItem>()
                {
                    OrderItemBuilder.Create().Price("0.3").Build()
                });

            CreateIyziupFormInitializeRequest request = CreateIyziupFormInitializeRequestBuilder.Create()
                .Price("0.3")
                .PaymentGroup(PaymentGroup.LISTING.ToString())
                .PaidPrice("0.4")
                .CallbackUrl("https://www.merchant.com/callback")
                .OrderItems(orderItems)
                .Build();

        }
    }
}