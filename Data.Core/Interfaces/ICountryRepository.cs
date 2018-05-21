using System;
using System.Threading.Tasks;
using Data.Core.Domain.Entities;

namespace Data.Core.Interfaces
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country> GetByIdAsync(Guid id);
        Task<Country> GetByNameAsync(string name);
    }
}
