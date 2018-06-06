using System.Threading.Tasks;
using Data.Core.Interfaces;

namespace WebAPI.Seeders
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly CitiesSeeder _citiesSeeder;
        private readonly CountriesSeeder _countriesSeeder;

        public DatabaseSeeder(CitiesSeeder cities, CountriesSeeder countries)
        {
            _citiesSeeder = cities;
            _countriesSeeder = countries;
        }

        public async Task<int> SeedAsync()
        {
            var totalRecords = await _countriesSeeder.SeedAsync() + await _citiesSeeder.SeedAsync();
            return totalRecords;
        }
    }
}
