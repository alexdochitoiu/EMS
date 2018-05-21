using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Core.Domain.Entities.Identity;

namespace Data.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetByAgeAsync(int age);
    }
}
