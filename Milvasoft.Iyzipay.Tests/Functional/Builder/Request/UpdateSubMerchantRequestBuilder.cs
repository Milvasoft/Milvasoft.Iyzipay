﻿using Milvasoft.Iyzipay.Request;

namespace Milvasoft.Iyzipay.Tests.Functional.Builder.Request
{
    public sealed class UpdateSubMerchantRequestBuilder : BaseRequestBuilder
    {
        private string _name = "John's market";
        private string _email = "email@submerchantemail.com";
        private string _gsmNumber = "+905350000000";
        private string _address = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        private string _iban = "TR180006200119000006672315";
        private string _currency = Model.Currency.TRY.ToString();
        private string _taxOffice;
        private string _contactName;
        private string _contactSurname;
        private string _legalCompanyTitle;
        private string _subMerchantKey;
        private string _identityNumber;
        private string _taxNumber;
        private string _swiftCode;

        private UpdateSubMerchantRequestBuilder()
        {
        }

        public static UpdateSubMerchantRequestBuilder Create()
        {
            return new UpdateSubMerchantRequestBuilder();
        }

        public UpdateSubMerchantRequestBuilder Name(string name)
        {
            _name = name;
            return this;
        }

        public UpdateSubMerchantRequestBuilder Email(string email)
        {
            _email = email;
            return this;
        }

        public UpdateSubMerchantRequestBuilder GsmNumber(string gsmNumber)
        {
            _gsmNumber = gsmNumber;
            return this;
        }

        public UpdateSubMerchantRequestBuilder Address(string address)
        {
            _address = address;
            return this;
        }

        public UpdateSubMerchantRequestBuilder Iban(string iban)
        {
            _iban = iban;
            return this;
        }

        public UpdateSubMerchantRequestBuilder TaxOffice(string taxOffice)
        {
            _taxOffice = taxOffice;
            return this;
        }

        public UpdateSubMerchantRequestBuilder ContactName(string contactName)
        {
            _contactName = contactName;
            return this;
        }

        public UpdateSubMerchantRequestBuilder ContactSurname(string contactSurname)
        {
            _contactSurname = contactSurname;
            return this;
        }

        public UpdateSubMerchantRequestBuilder LegalCompanyTitle(string legalCompanyTitle)
        {
            _legalCompanyTitle = legalCompanyTitle;
            return this;
        }

        public UpdateSubMerchantRequestBuilder SubMerchantKey(string subMerchantKey)
        {
            _subMerchantKey = subMerchantKey;
            return this;
        }

        public UpdateSubMerchantRequestBuilder IdentityNumber(string identityNumber)
        {
            _identityNumber = identityNumber;
            return this;
        }

        public UpdateSubMerchantRequestBuilder TaxNumber(string taxNumber)
        {
            _taxNumber = taxNumber;
            return this;
        }

        public UpdateSubMerchantRequestBuilder Currency(string currency)
        {
            _currency = currency;
            return this;
        }

        public UpdateSubMerchantRequestBuilder SwiftCode(string swiftCode)
        {
            _swiftCode = swiftCode;
            return this;
        }

        public UpdateSubMerchantRequest Build()
        {
            UpdateSubMerchantRequest updateSubMerchantRequest = new()
            {
                Locale = Locale,
                ConversationId = ConversationId,
                Name = _name,
                Email = _email,
                GsmNumber = _gsmNumber,
                Address = _address,
                Iban = _iban,
                TaxOffice = _taxOffice,
                ContactName = _contactName,
                ContactSurname = _contactSurname,
                LegalCompanyTitle = _legalCompanyTitle,
                SubMerchantKey = _subMerchantKey,
                IdentityNumber = _identityNumber,
                TaxNumber = _taxNumber,
                Currency = _currency,
                SwiftCode = _swiftCode
            };
            return updateSubMerchantRequest;
        }
    }
}
