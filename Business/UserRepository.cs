using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseService _databaseService;

        public UserRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<List<User>> GetAll()
        {
            return await _databaseService.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _databaseService.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> FindByAsync(Expression<Func<User, bool>> predicate)
        {
            return await _databaseService.Users.Where(predicate).ToListAsync();
        }

        public async Task Add(User entity)
        {
            _databaseService.Users.Add(entity);
            await _databaseService.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user == null) return false;
            _databaseService.Users.Remove(user);
            await _databaseService.SaveChangesAsync();
            return true;
        }

        public async Task Edit(User entity)
        {
            _databaseService.Users.Update(entity);
            await _databaseService.SaveChangesAsync();
        }

        public async Task<List<User>> GetByFirstName(string firstName)
        {
            return await _databaseService.Users.Where(u => u.FirstName == firstName).ToListAsync();
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
