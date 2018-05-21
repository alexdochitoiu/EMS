using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain.Entities.Identity;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetByAgeAsync(int age)
        {
            return await FindAsync(u => u.Age == age,
                t => t.Include(user => user.Address)
                        .ThenInclude(address => address.Country)
                    .Include(user => user.Address)
                        .ThenInclude(address => address.City));
        }

        public async Task<ApplicationUser> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(t => t.Address)
                    .ThenInclude(a => a.Country)
                .Include(t => t.Address)
                    .ThenInclude(a => a.City)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
