﻿using Milvasoft.Iyzipay.Model;

namespace Milvasoft.Iyzipay.Tests.Functional.Builder
{
    public sealed class AddressBuilder
    {
        private string _description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        private string _zipCode = "34742";
        private string _contactName = "Jane Doe";
        private string _city = "Istanbul";
        private string _country = "Turkey";

        private AddressBuilder()
        {
        }

        public static AddressBuilder Create()
        {
            return new AddressBuilder();
        }

        public AddressBuilder Description(string description)
        {
            _description = description;
            return this;
        }

        public AddressBuilder ZipCode(string zipCode)
        {
            _zipCode = zipCode;
            return this;
        }

        public AddressBuilder ContactName(string contactName)
        {
            _contactName = contactName;
            return this;
        }

        public AddressBuilder City(string city)
        {
            _city = city;
            return this;
        }

        public AddressBuilder Country(string country)
        {
            _country = country;
            return this;
        }

        public Address Build()
        {
            Address addressModel = new()
            {
                Description = _description,
                ZipCode = _zipCode,
                ContactName = _contactName,
                City = _city,
                Country = _country
            };
            return addressModel;
        }
    }
}
