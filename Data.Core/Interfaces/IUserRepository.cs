using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Core.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetByIdAsync(Guid id);
        Task<IEnumerable<ApplicationUser>> GetByAgeAsync(int age);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByUsernameAsync(string username);
        Task<EntityEntry<UserToken>> AddUserTokenAsync(UserToken userToken);
        Task<UserToken> GetUserTokenAsync(Guid userId);
        Task<List<ApplicationUser>> GetUsersWithinARadiusAsync(double centerLat, double centerLng, double km);
    }
}
