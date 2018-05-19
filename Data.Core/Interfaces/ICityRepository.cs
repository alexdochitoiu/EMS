using System;
using System.Threading.Tasks;
using Data.Core.Domain.Entities;

namespace Data.Core.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<City> GetById(Guid id);
        Task<City> GetByName(string name);
    }
}
