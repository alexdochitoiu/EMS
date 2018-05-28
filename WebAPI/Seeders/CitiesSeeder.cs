using System.Collections.Generic;
using System.Linq;
using Data.Core.Domain.Entities;
using Data.Persistence;

namespace WebAPI.Seeders
{
    public class CitiesSeeder
    {
        private readonly ApplicationDbContext _context;

        public CitiesSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Cities.Any()) return;
            if (!_context.Countries.Any()) return;

            var countries = _context.Countries.ToList();
            _context.Cities.AddRange(GetCities(countries));
            _context.SaveChangesAsync();
        }

        private static IEnumerable<City> GetCities(IEnumerable<Country> countries)
        {
            var cities = new List<City>();
            var enumerable = countries as Country[] ?? countries.ToArray();

            // Romania
            var country = enumerable.FirstOrDefault(c => c.Name.Equals("Romania"));
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

            // Germany
            country = enumerable.FirstOrDefault(c => c.Name.Equals("Germany"));
            if (country == null) return cities;
            cities.Add(City.Create("Berlin", "B", 0, 0, country.Id));
            cities.Add(City.Create("Hamburg", "HH", 0, 0, country.Id));
            cities.Add(City.Create("Munich", "M", 0, 0, country.Id));
            cities.Add(City.Create("Stuttgart", "S", 0, 0, country.Id));
            cities.Add(City.Create("Wolfsburg", "WOB", 0, 0, country.Id));
            cities.Add(City.Create("Frankfurt", "FH", 0, 0, country.Id));
            cities.Add(City.Create("Hanover", "H", 0, 0, country.Id));
            cities.Add(City.Create("Hofheim", "HOH", 0, 0, country.Id));
            cities.Add(City.Create("Ingolstadt", "IN", 0, 0, country.Id));
            cities.Add(City.Create("Karlsruhe", "KH", 0, 0, country.Id));
            cities.Add(City.Create("Leipzig", "L", 0, 0, country.Id));
            cities.Add(City.Create("Regensberg", "R", 0, 0, country.Id));
            cities.Add(City.Create("Freiberg", "FG", 0, 0, country.Id));

            //Italy
            country = enumerable.FirstOrDefault(c => c.Name.Equals("Italy"));
            if (country == null) return cities;
            cities.Add(City.Create("Napoli", "NA", 0, 0, country.Id));
            cities.Add(City.Create("Bologna", "BO", 0, 0, country.Id));
            cities.Add(City.Create("Parma", "PR", 0, 0, country.Id));
            cities.Add(City.Create("Bari", "BA", 0, 0, country.Id));
            cities.Add(City.Create("Reggio Emilia", "RE", 0, 0, country.Id));
            cities.Add(City.Create("Roma", "RM", 0, 0, country.Id));

            return cities;
        }
    }
}
