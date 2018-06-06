using System;
using EnsureThat;

namespace Data.Core.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public Country Country { get; set; }
        public Guid CountryId { get; private set; }

        public static City Create(string name, string abbreviation, double latitude, double longitude, Guid countryId)
        {
            var city = new City
            {
                Id = new Guid(),
                Created = DateTime.Now
            };
            city.Update(name, abbreviation, latitude, longitude, countryId);
            return city;
        }

        public void Update(string name, string abbreviation, double latitude, double longitude, Guid countryId)
        {
            Validate(name, abbreviation, latitude, longitude, countryId);

            Name = name;
            Abbreviation = abbreviation;
            Latitude = latitude;
            Longitude = longitude;
            CountryId = countryId;
            Modified = DateTime.Now;
        }

        private static void Validate(string name, string abbreviation, double latitude, double longitude, Guid countryId)
        {
            Ensure.That(name).IsNotNullOrEmpty();
            Ensure.That(abbreviation).IsNotNullOrEmpty();
            Ensure.That(latitude).IsInRange(-90, 90);
            Ensure.That(longitude).IsInRange(-180, 180);
            Ensure.That(countryId).IsNotEmpty();
        }
    }
}
