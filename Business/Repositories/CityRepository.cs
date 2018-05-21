using System;
using System.Threading.Tasks;
using Data.Core.Domain.Entities;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<City> GetByIdAsync(Guid id) =>
            await _context.Cities
                    .FirstOrDefaultAsync(t => t.Id == id);

        public async Task<City> GetByNameAsync(string name) => 
            await _context.Cities
                    .FirstOrDefaultAsync(t => t.Name == name);
    }
}
