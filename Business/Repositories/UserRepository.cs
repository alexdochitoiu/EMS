using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain.Entities;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DatabaseContext _databaseService;

        public UserRepository(DatabaseContext databaseService) : base(databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IEnumerable<User>> GetByAge(int age)
        {
            return await Find(u => u.Age == age,
                                     t => t.Include(user => user.Address)
                                                .ThenInclude(address => address.Country)
                                           .Include(user => user.Address)
                                                .ThenInclude(address => address.City));
        }

        public async Task<User> GetById(Guid id)
        {
            return await _databaseService.Users
                .Include(t => t.Address)
                    .ThenInclude(a => a.Country)
                .Include(t => t.Address)
                    .ThenInclude(a => a.City)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
