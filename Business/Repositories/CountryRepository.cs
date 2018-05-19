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
        private readonly DatabaseContext _databaseService;

        public CountryRepository(DatabaseContext databaseService) : base(databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Country> GetById(Guid id) =>
            await _databaseService.Countries
                    .Include(t => t.Cities)
                    .FirstOrDefaultAsync(t => t.Id == id);

        public async Task<Country> GetByName(string name) => 
            await _databaseService.Countries
                    .FirstOrDefaultAsync(t => t.Name == name);
    }
}
