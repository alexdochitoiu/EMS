using System;
using EnsureThat;

namespace Data.Core.Domain
{
    public class City
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public static City Create(string name, string abbreviation, double latitude, double longitude)
        {
            Validate(name, abbreviation, latitude, longitude);
            var city = new City
            {
                Id = new Guid()
            };
            city.Update(name, abbreviation, latitude, longitude);
            return city;
        }

        public void Update(string name, string abbreviation, double latitude, double longitude)
        {
            Validate(name, abbreviation, latitude, longitude);

            Name = name;
            Abbreviation = abbreviation;
            Latitude = latitude;
            Longitude = longitude;
        }

        private static void Validate(string name, string abbreviation, double latitude, double longitude)
        {
            Ensure.That(name).IsNotNullOrEmpty();
            Ensure.That(abbreviation).IsNotNullOrEmpty();
            Ensure.That(latitude).IsInRange(-90, 90);
            Ensure.That(longitude).IsInRange(-180, 180);
        }
    }
}
