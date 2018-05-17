using System;
using System.Threading.Tasks;
using Data.Core.Domain;
using System.Collections.Generic;

namespace Data.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetById(Guid id);
        Task<IEnumerable<User>> GetByAge(int age);
    }
}
