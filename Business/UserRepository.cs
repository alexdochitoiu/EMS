using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseService _databaseService;

        public UserRepository(IDatabaseService databaseService)
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

        public void Add(User entity)
        {
            _databaseService.Users.Add(entity);
            _databaseService.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user == null) return false;
            _databaseService.Users.Remove(user);
            _databaseService.SaveChangesAsync();
            return true;
        }

        public void Edit(User entity)
        {
            _databaseService.Users.Update(entity);
            _databaseService.SaveChangesAsync();
        }

        public Task<IQueryable<User>> GetByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetByAge(int age)
        {
            throw new NotImplementedException();
        }
    }
}
