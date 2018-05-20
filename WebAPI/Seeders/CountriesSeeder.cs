using System.Collections.Generic;
using System.Linq;
using Data.Core.Domain.Entities;
using Data.Persistence;

namespace WebAPI.Seeders
{
    public class CountriesSeeder
    {
        private readonly IdentityContext _identityContext;

        public CountriesSeeder(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public void Seed()
        {
            if (_identityContext.Countries.Any()) return;

            _identityContext.Countries.AddRange(GetCountries());
            _identityContext.SaveChangesAsync();
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
