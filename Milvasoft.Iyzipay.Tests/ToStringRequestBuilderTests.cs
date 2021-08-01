﻿using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Utils.Concrete;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Milvasoft.Iyzipay.Tests
{
    public class ToStringRequestBuilderTests
    {
        [Test]
        public void Should_Append_And_Convert_Object_To_String()
        {
            string requestString = ToStringRequestBuilder.NewInstance()
                .Append("conversationId", "123456")
                .Append("locale", Locale.TR.ToString())
                .Append("price", "1.0").GetRequestString();

            Assert.AreEqual("[conversationId=123456,locale=tr,price=1.0]", requestString);
        }

        [Test]
        public void Should_Convert_To_Nothing()
        {
            string requestString = ToStringRequestBuilder.NewInstance().GetRequestString();
            Assert.AreEqual("[]", requestString);
        }
    }
}
