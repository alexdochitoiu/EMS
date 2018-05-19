using System.Collections.Generic;
using System.Linq;
using Data.Core.Domain.Entities;
using Data.Persistence.Interfaces;

namespace WebAPI.Seeders
{
    public class CountriesSeeder
    {
        private readonly IDatabaseContext _databaseContext;

        public CountriesSeeder(IDatabaseContext databaseService)
        {
            _databaseContext = databaseService;
        }

        public void Seed()
        {
            if (_databaseContext.Countries.Any()) return;

            _databaseContext.Countries.AddRange(GetCountries());
            _databaseContext.SaveChanges();
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
