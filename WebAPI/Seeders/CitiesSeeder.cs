using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<int> SeedAsync()
        {
            if (_context.Cities.Any()) return -1;
            if (!_context.Countries.Any()) return -1;

            var countries = _context.Countries.ToList();
            await _context.Cities.AddRangeAsync(GetCities(countries));
            return await _context.SaveChangesAsync();
        }

        private static IEnumerable<City> GetCities(IEnumerable<Country> countries)
        {
            var cities = new List<City>();
            var enumerable = countries as Country[] ?? countries.ToArray();

            // Romania
            var country = enumerable.FirstOrDefault(c => c.Name.Equals("Romania"));
            if (country == null) return cities;
            cities.Add(City.Create("Alba", "TBD", 46.0729378, 23.5796009, country.Id));
            cities.Add(City.Create("Arad", "TBD", 46.1751797, 21.2363177, country.Id));
            cities.Add(City.Create("Arges", "TBD", 44.9473292, 24.4286966, country.Id));
            cities.Add(City.Create("Bacau", "BC", 46.5640211, 26.8388194, country.Id));
            cities.Add(City.Create("Bihor", "TBD", 46.9468536, 21.6115183, country.Id));
            cities.Add(City.Create("Botosani", "TBD", 47.745104, 26.665344, country.Id));
            cities.Add(City.Create("Brasov", "TBD", 45.657233, 25.5970273, country.Id));
            cities.Add(City.Create("Buzau", "TBD", 45.148632, 26.7791741, country.Id));
            cities.Add(City.Create("Caras-Severin", "TBD", 45.1562563, 21.3712731, country.Id));
            cities.Add(City.Create("Calarasi", "TBD", 44.204898, 27.301031, country.Id));
            cities.Add(City.Create("Cluj", "TBD", 46.770042, 23.621971, country.Id));
            cities.Add(City.Create("Constanta", "TBD", 44.167320, 28.627604, country.Id));
            cities.Add(City.Create("Covasna", "TBD", 45.848431, 26.175206, country.Id));
            cities.Add(City.Create("Dambovita", "TBD", 44.925805, 25.460762, country.Id));
            cities.Add(City.Create("Dolj", "TBD", 44.320966, 23.771827, country.Id));
            cities.Add(City.Create("Gorj", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Harghita", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Hunedoara", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Ialomita", "TBD", 0, 0, country.Id));
            cities.Add(City.Create("Iasi", "IS", 47.154313, 27.593889, country.Id));
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

            //// Germany
            //country = enumerable.FirstOrDefault(c => c.Name.Equals("Germany"));
            //if (country == null) return cities;
            //cities.Add(City.Create("Berlin", "B", 0, 0, country.Id));
            //cities.Add(City.Create("Hamburg", "HH", 0, 0, country.Id));
            //cities.Add(City.Create("Munich", "M", 0, 0, country.Id));
            //cities.Add(City.Create("Stuttgart", "S", 0, 0, country.Id));
            //cities.Add(City.Create("Wolfsburg", "WOB", 0, 0, country.Id));
            //cities.Add(City.Create("Frankfurt", "FH", 0, 0, country.Id));
            //cities.Add(City.Create("Hanover", "H", 0, 0, country.Id));
            //cities.Add(City.Create("Hofheim", "HOH", 0, 0, country.Id));
            //cities.Add(City.Create("Ingolstadt", "IN", 0, 0, country.Id));
            //cities.Add(City.Create("Karlsruhe", "KH", 0, 0, country.Id));
            //cities.Add(City.Create("Leipzig", "L", 0, 0, country.Id));
            //cities.Add(City.Create("Regensberg", "R", 0, 0, country.Id));
            //cities.Add(City.Create("Freiberg", "FG", 0, 0, country.Id));

            ////Italy
            //country = enumerable.FirstOrDefault(c => c.Name.Equals("Italy"));
            //if (country == null) return cities;
            //cities.Add(City.Create("Napoli", "NA", 0, 0, country.Id));
            //cities.Add(City.Create("Bologna", "BO", 0, 0, country.Id));
            //cities.Add(City.Create("Parma", "PR", 0, 0, country.Id));
            //cities.Add(City.Create("Bari", "BA", 0, 0, country.Id));
            //cities.Add(City.Create("Reggio Emilia", "RE", 0, 0, country.Id));
            //cities.Add(City.Create("Roma", "RM", 0, 0, country.Id));
            //cities.Add(City.Create("Venice", "VCE", 0, 0, country.Id));

            return cities;
        }
    }
}
