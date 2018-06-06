using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Core.Domain.Entities.Identity;

namespace Data.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetByIdAsync(Guid id);
        Task<IEnumerable<ApplicationUser>> GetByAgeAsync(int age);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByUsernameAsync(string username);
    }
}
