using System;

namespace Data.Core.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Country Country { get; private set; }
        public City City { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string ZipCode { get; private set; }

        public static Address Create(Country country, City city, string street, string number, string zipCode)
        {
            var address = new Address
            {
                Id = new Guid(),
                Created = DateTime.Now
            };
            address.Update(country, city, street, number, zipCode);
            return address;
        }

        public void Update(Country country, City city, string street, string number, string zipCode)
        {
            Country = country;
            City = city;
            Street = street;
            Number = number;
            ZipCode = zipCode;
            Modified = DateTime.Now;
        }
    }
}
