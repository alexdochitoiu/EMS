using System.Threading.Tasks;
using Business.Repositories;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUserRepository Users { get; }
        public ICountryRepository Countries { get; }
        public ICityRepository Cities { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            
            Users = new UserRepository(_context);
            Countries = new CountryRepository(_context);
            Cities = new CityRepository(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
