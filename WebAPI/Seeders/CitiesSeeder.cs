using System.Collections.Generic;
using System.Linq;
using Data.Core.Domain.Entities;
using Data.Persistence;

namespace WebAPI.Seeders
{
    public class CitiesSeeder
    {
        private readonly IdentityContext _identityContext;

        public CitiesSeeder(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public void Seed()
        {
            if (_identityContext.Cities.Any()) return;
            if (!_identityContext.Countries.Any()) return;

            var countries = _identityContext.Countries.ToList();
            _identityContext.Cities.AddRange(GetCities(countries));
            _identityContext.SaveChangesAsync();
        }

        private static IEnumerable<City> GetCities(IEnumerable<Country> countries)
        {
            var cities = new List<City>();

            // Romania
            var country = countries.FirstOrDefault(c => c.Name.Equals("Romania"));
            if (country == null) return cities;
            cities.Add(City.Create("Alba", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Arad", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Arges", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Bacau", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Bihor", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Botosani", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Brasov", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Buzau", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Caras-Severin", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Calarasi", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Cluj", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Constanta", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Covasna", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Dambovita", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Dolj", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Gorj", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Harghita", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Hunedoara", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Ialomita", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Iasi", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Ilfov", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Maramures", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Mehedinti", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Mures", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Neamt", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Olt", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Prahova", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Satu Mare", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Sibiu", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Suceava", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Teleorman", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Timis", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Vaslui", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Valcea", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Vrancea", "TBD", 0, 0, country.Id));
            return cities;
        }
    }
}
