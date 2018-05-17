using System;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly DatabaseService _databaseService;

        public CityRepository(DatabaseService databaseService) : base(databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<City> GetById(Guid id) =>
            await _databaseService.Cities
                    .FirstOrDefaultAsync(t => t.Id == id);

        public async Task<City> GetByName(string name) => 
            await _databaseService.Cities
                    .FirstOrDefaultAsync(t => t.Name == name);
    }
}
