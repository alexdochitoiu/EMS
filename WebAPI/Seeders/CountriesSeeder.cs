using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain.Entities;
using Data.Persistence;
using Microsoft.EntityFrameworkCore.Internal;

namespace WebAPI.Seeders
{
    public class CountriesSeeder
    {
        private readonly ApplicationDbContext _context;

        public CountriesSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SeedAsync()
        {
            if (_context.Countries.Any()) return -1;
            await _context.Countries.AddRangeAsync(GetCountries());
            return await _context.SaveChangesAsync();
        }

        private static IEnumerable<Country> GetCountries()
        {
            return new List<Country>
            {
                Country.Create("Romania", "RO"),
                Country.Create("Germany", "DE"),
                Country.Create("Italy", "IT"),
                Country.Create("United Kingdom", "UK"),
                Country.Create("Spain", "ESP"),
                Country.Create("France", "FR"),
                Country.Create("Netherlands", "NE"),
                Country.Create("Poland", "PL"),
                Country.Create("Russia", "RU"),
                Country.Create("Turkey", "TR"),
                Country.Create("Austria", "AU")
            };
        }
    }
}
