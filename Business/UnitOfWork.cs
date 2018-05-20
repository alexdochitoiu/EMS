using System.Threading.Tasks;
using Business.Repositories;
using Data.Core.Domain.Entities.Identity;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityContext _identityContext;

        public IUserRepository Users { get; }
        public ICountryRepository Countries { get; }
        public ICityRepository Cities { get; }

        public UnitOfWork(IdentityContext identityContext)
        {
            _identityContext = identityContext;
            
            Users = new UserRepository(_identityContext);
            Countries = new CountryRepository(_identityContext);
            Cities = new CityRepository(_identityContext);
        }

        public async Task<int> Complete() => await _identityContext.SaveChangesAsync();

        public void Dispose() => _identityContext.Dispose();
    }
}
