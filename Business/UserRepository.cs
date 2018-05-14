using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DatabaseService _databaseService;

        public UserRepository(DatabaseService databaseService) : base(databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<User> GetById(Guid id)
        {
            return await _databaseService.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        
        public async Task<List<User>> GetByFirstName(string firstName)
        {
            return await _databaseService.Users.Where(u => u.FirstName.StartsWith(firstName)).ToListAsync();
        }

        public async Task<List<User>> GetByLastName(string lastName)
        {
            return await _databaseService.Users.Where(u => u.LastName == lastName).ToListAsync();
        }

        public async Task<List<User>> GetByAge(int age)
        {
            return await _databaseService.Users.Where(u => u.Age == age).ToListAsync();
        }
    }
}
