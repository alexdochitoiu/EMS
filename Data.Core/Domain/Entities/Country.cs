using System;
using System.Collections.Generic;
using EnsureThat;

namespace Data.Core.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }
        public IReadOnlyCollection<City> Cities { get; private set; }

        public static Country Create(string name, string abbreviation)
        {
            var country = new Country
            {
                Id = new Guid(),
                Cities = new List<City>(),
                Created = DateTime.Now
            };
            country.Update(name, abbreviation);
            return country;
        }

        public void Update(string name, string abbreviation)
        {
            Validate(name, abbreviation);
            Name = name;
            Abbreviation = abbreviation;
            Modified = DateTime.Now;
        }

        private static void Validate(string name, string abbreviation)
        {
            Ensure.That(name).IsNotNullOrEmpty();
            Ensure.That(abbreviation).IsNotNullOrEmpty();
        }
    }
}
