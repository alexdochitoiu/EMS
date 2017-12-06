using EnsureThat;
using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class Country
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }
        public IReadOnlyCollection<City> Cities { get; private set; }

        public static Country Create(string name, string abbreviation)
        {
            Validate(name, abbreviation);
            var country = new Country
            {
                Id = new Guid(),
                Cities = new List<City>()
            };
            country.Update(name, abbreviation);
            return country;
        }

        public void Update(string name, string abbreviation)
        {
            Validate(name, abbreviation);
            Name = name;
            Abbreviation = abbreviation;
        }

        private static void Validate(string name, string abbreviation)
        {
            Ensure.That(name).IsNotNullOrEmpty();
            Ensure.That(abbreviation).IsNotNullOrEmpty();
        }
    }
}
