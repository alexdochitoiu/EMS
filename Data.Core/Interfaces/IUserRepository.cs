using System;
using System.Threading.Tasks;
using Data.Core.Domain;
using System.Collections.Generic;

namespace Data.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetByFirstName(string firstName);
        Task<List<User>> GetByLastName(string lastName);
        Task<List<User>> GetByAge(int age);
        Task<User> GetById(Guid id);
    }
}
