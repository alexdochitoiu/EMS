using System;
using System.Threading.Tasks;
using Data.Core.Domain.Entities;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Country> GetByIdAsync(Guid id) =>
            await _context.Countries
                    .Include(t => t.Cities)
                    .FirstOrDefaultAsync(t => t.Id == id);

        public async Task<Country> GetByNameAsync(string name) => 
            await _context.Countries
                    .Include(t => t.Cities)
                    .FirstOrDefaultAsync(t => t.Name == name);
    }
}
