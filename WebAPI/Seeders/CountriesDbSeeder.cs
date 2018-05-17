using System.Collections.Generic;
using System.Linq;
using Data.Core.Domain;
using Data.Persistence;

namespace WebAPI.Seeders
{
    public class CountriesDbSeeder
    {
        private readonly DatabaseService _databaseService;

        public CountriesDbSeeder(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public void Seed()
        {
            if (_databaseService.Countries.Any()) return;

            _databaseService.Countries.AddRange(GetCountries());
            _databaseService.SaveChanges();
        }

        private static IEnumerable<Country> GetCountries()
        {
            return new List<Country>
            {
                Country.Create("Romania", "RO")
            };
        }
    }
}
