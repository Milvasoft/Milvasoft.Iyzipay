﻿using Milvasoft.Iyzipay.Model;

namespace Milvasoft.Iyzipay.Tests.Functional.Builder
{
    public sealed class CardInformationBuilder
    {
        private string _cardAlias = "card alias";
        private string _cardNumber = "5528790000000008";
        private string _expireYear = "2030";
        private string _expireMonth = "12";
        private string _cardHolderName = "John Doe";

        private CardInformationBuilder()
        {
        }

        public static CardInformationBuilder Create()
        {
            return new CardInformationBuilder();
        }

        public CardInformationBuilder CardAlias(string cardAlias)
        {
            _cardAlias = cardAlias;
            return this;
        }

        public CardInformationBuilder CardNumber(string cardNumber)
        {
            _cardNumber = cardNumber;
            return this;
        }

        public CardInformationBuilder ExpireYear(string expireYear)
        {
            _expireYear = expireYear;
            return this;
        }

        public CardInformationBuilder ExpireMonth(string expireMonth)
        {
            _expireMonth = expireMonth;
            return this;
        }

        public CardInformationBuilder CardHolderName(string cardHolderName)
        {
            _cardHolderName = cardHolderName;
            return this;
        }

        public CardInformation Build()
        {
            CardInformation cardInformation = new()
            {
                CardAlias = _cardAlias,
                CardNumber = _cardNumber,
                ExpireYear = _expireYear,
                ExpireMonth = _expireMonth,
                CardHolderName = _cardHolderName
            };
            return cardInformation;
        }
    }
}
