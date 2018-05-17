using System;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country> GetById(Guid id);
        Task<Country> GetByName(string name);
    }
}
