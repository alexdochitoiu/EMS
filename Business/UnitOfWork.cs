using System.Threading.Tasks;
using Business.Repositories;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseService _databaseService;

        public UnitOfWork(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Users = new UserRepository(_databaseService);
            Countries = new CountryRepository(_databaseService);
            Cities = new CityRepository(_databaseService);
        }

        public IUserRepository Users { get; }
        public ICountryRepository Countries { get; }
        public ICityRepository Cities { get; }

        public async Task<int> Complete() => await _databaseService.SaveChangesAsync();

        public void Dispose() => _databaseService.Dispose();
    }
}
