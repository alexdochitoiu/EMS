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
        private readonly IdentityContext _identityContext;

        public CountryRepository(IdentityContext identityContext) : base(identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<Country> GetById(Guid id) =>
            await _identityContext.Countries
                    .Include(t => t.Cities)
                    .FirstOrDefaultAsync(t => t.Id == id);

        public async Task<Country> GetByName(string name) => 
            await _identityContext.Countries
                    .FirstOrDefaultAsync(t => t.Name == name);
    }
}
