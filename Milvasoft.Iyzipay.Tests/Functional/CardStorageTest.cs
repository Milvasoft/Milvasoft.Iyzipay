﻿using Milvasoft.Iyzipay.Model;
using Milvasoft.Iyzipay.Request;
using Milvasoft.Iyzipay.Tests.Functional.Builder;
using Milvasoft.Iyzipay.Tests.Functional.Builder.Request;
using Milvasoft.Iyzipay.Tests.Functional.Util;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Milvasoft.Iyzipay.Tests.Functional
{
    public class CardStorageTest : BaseTest
    {
        [Test]
        public async Task Should_Create_User_And_Add_Card()
        {
            string externalUserId = RandomGenerator.RandomId;
            CardInformation cardInformation = CardInformationBuilder
                .Create()
                .Build();

            CreateCardRequest createCardRequest = CreateCardRequestBuilder.Create()
                .Card(cardInformation)
                .ExternalId(externalUserId)
                .Email("email@email.com")
                .Build();

            Card card = new(RestHttpClient);

            card = await card.CreateAsync(createCardRequest).ConfigureAwait(false);

            PrintResponse(card);

            Assert.AreEqual(Locale.TR.ToString(), card.Locale);
            Assert.AreEqual(Status.SUCCESS.ToString(), card.Status);
            Assert.NotNull(card.SystemTime);
            Assert.AreEqual("123456789", card.ConversationId);
            Assert.AreEqual("email@email.com", card.Email);
            Assert.AreEqual("552879", card.BinNumber);
            Assert.AreEqual("card alias", card.CardAlias);
            Assert.AreEqual("CREDIT_CARD", card.CardType);
            Assert.AreEqual("MASTER_CARD", card.CardAssociation);
            Assert.AreEqual("Paraf", card.CardFamily);
            Assert.AreEqual("Halk Bankası", card.CardBankName);
            Assert.True(card.CardBankCode.Equals(12L));
        }

        [Test]
        public async Task Should_Create_Card_And_Add_Card_To_Existing_User()
        {
            string externalUserId = RandomGenerator.RandomId;
            CardInformation cardInformation = CardInformationBuilder.Create()
                .Build();

            CreateCardRequest cardRequest = CreateCardRequestBuilder.Create()
                .Card(cardInformation)
                .ExternalId(externalUserId)
                .Email("email@email.com")
                .Build();

            Card firstCard = new(RestHttpClient);

            firstCard = await firstCard.CreateAsync(cardRequest).ConfigureAwait(false);
            string cardUserKey = firstCard.CardUserKey;

            CreateCardRequest request = CreateCardRequestBuilder.Create()
                .Card(cardInformation)
                .CardUserKey(cardUserKey)
                .ExternalId(externalUserId)
                .Build();

            Card card = new(RestHttpClient);

            card = await card.CreateAsync(request).ConfigureAwait(false);

            PrintResponse(card);

            Assert.AreEqual(Locale.TR.ToString(), card.Locale);
            Assert.AreEqual(Status.SUCCESS.ToString(), card.Status);
            Assert.NotNull(card.SystemTime);
            Assert.AreEqual("123456789", card.ConversationId);
            Assert.AreEqual("552879", card.BinNumber);
            Assert.AreEqual("card alias", card.CardAlias);
            Assert.AreEqual("CREDIT_CARD", card.CardType);
            Assert.AreEqual("MASTER_CARD", card.CardAssociation);
            Assert.AreEqual("Paraf", card.CardFamily);
            Assert.AreEqual("Halk Bankası", card.CardBankName);
            Assert.AreEqual(externalUserId, card.ExternalId);
            Assert.True(card.CardBankCode.Equals(12L));
        }

        [Test]
        public async Task Should_Delete_Card()
        {
            Card card = await CreateCard().ConfigureAwait(false);

            DeleteCardRequest deleteCardRequest = new()
            {
                CardToken = card.CardToken,
                CardUserKey = card.CardUserKey
            };

            Card deletedCard = new(RestHttpClient);

            deletedCard = await deletedCard.DeleteAsync(deleteCardRequest).ConfigureAwait(false);

            PrintResponse(deletedCard);

            Assert.AreEqual(Status.SUCCESS.ToString(), deletedCard.Status);
            Assert.AreEqual(Locale.TR.ToString(), deletedCard.Locale);
            Assert.NotNull(deletedCard.SystemTime);
            Assert.Null(deletedCard.ErrorCode);
            Assert.Null(deletedCard.ErrorMessage);
            Assert.Null(deletedCard.ErrorGroup);
            Assert.Null(deletedCard.BinNumber);
            Assert.Null(deletedCard.CardAlias);
            Assert.Null(deletedCard.CardType);
            Assert.Null(deletedCard.CardAssociation);
            Assert.Null(deletedCard.CardFamily);
            Assert.Null(deletedCard.CardBankName);
            Assert.Null(deletedCard.CardBankCode);
            Assert.Null(deletedCard.CardUserKey);
            Assert.Null(deletedCard.CardToken);
            Assert.Null(deletedCard.Email);
            Assert.Null(deletedCard.ExternalId);
        }

        [Test]
        public async Task Chould_Retrieve_Card()
        {
            Card card = await CreateCard().ConfigureAwait(false);

            RetrieveCardListRequest request = new()
            {
                Locale = Locale.TR.ToString(),
                ConversationId = "123456789",
                CardUserKey = card.CardUserKey
            };

            CardList cardList = new(RestHttpClient);

            cardList = await cardList.RetrieveAsync(request).ConfigureAwait(false);

            PrintResponse(cardList);

            Assert.AreEqual(Status.SUCCESS.ToString(), cardList.Status);
            Assert.AreEqual(Locale.TR.ToString(), cardList.Locale);
            Assert.AreEqual("123456789", cardList.ConversationId);
            Assert.NotNull(cardList.SystemTime);
            Assert.Null(cardList.ErrorCode);
            Assert.Null(cardList.ErrorMessage);
            Assert.Null(cardList.ErrorGroup);
            Assert.NotNull(cardList.CardDetails);
            Assert.False(cardList.CardDetails.Count == 0);
            Assert.NotNull(cardList.CardUserKey);
        }

        private async Task<Card> CreateCard()
        {
            CardInformation cardInformation = CardInformationBuilder.Create()
                .Build();

            CreateCardRequest cardRequest = CreateCardRequestBuilder.Create()
                .Card(cardInformation)
                .Email("email@email.com")
                .Build();

            Card card = new(RestHttpClient);

            return await card.CreateAsync(cardRequest).ConfigureAwait(false);
        }
    }
}
