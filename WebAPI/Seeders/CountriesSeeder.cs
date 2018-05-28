using System.Collections.Generic;
using System.Linq;
using Data.Core.Domain.Entities;
using Data.Persistence;

namespace WebAPI.Seeders
{
    public class CountriesSeeder
    {
        private readonly ApplicationDbContext _context;

        public CountriesSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Countries.Any()) return;

            _context.Countries.AddRange(GetCountries());
            _context.SaveChangesAsync();
        }

        private static IEnumerable<Country> GetCountries()
        {
            return new List<Country>
            {
                Country.Create("Romania", "RO"),
                Country.Create("Germany", "RO"),
                Country.Create("Italy", "RO")
            };
        }
    }
}
