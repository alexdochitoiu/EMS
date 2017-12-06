using System;
using EnsureThat;

namespace Data.Core.Domain
{
    public class Address
    {
        public Guid Id { get; private set; }
        public Country Country { get; private set; }
        public City City { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string ZipCode { get; private set; }

        public static Address Create(Country country, City city, string street, string number, string zipCode)
        {
            Validate(country, city, street, number, zipCode);
            var address = new Address
            {
                Id = new Guid()
            };
            address.Update(country, city, street, number, zipCode);
            return address;
        }

        public void Update(Country country, City city, string street, string number, string zipCode)
        {
            Validate(country, city, street, number, zipCode);
            Country = country;
            City = city;
            Street = street;
            Number = number;
            ZipCode = zipCode;
        }
        
        private static void Validate(Country country, City city, string street, string number, string zipCode)
        {
            Ensure.That(country).IsNotNull();
            Ensure.That(city).IsNotNull();
            Ensure.That(street).IsNotNullOrEmpty();
            Ensure.That(number).IsNotNullOrEmpty();
            Ensure.That(zipCode).IsNotNullOrEmpty();
        }
    }
}
