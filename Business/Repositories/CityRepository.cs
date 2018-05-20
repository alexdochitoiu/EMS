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
        private readonly IdentityContext _identityContext;

        public CityRepository(IdentityContext identityContext) : base(identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<City> GetById(Guid id) =>
            await _identityContext.Cities
                    .FirstOrDefaultAsync(t => t.Id == id);

        public async Task<City> GetByName(string name) => 
            await _identityContext.Cities
                    .FirstOrDefaultAsync(t => t.Name == name);
    }
}
