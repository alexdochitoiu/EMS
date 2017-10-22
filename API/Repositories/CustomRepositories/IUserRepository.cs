using System.Collections.Generic;
using API.Models.Entities;

namespace API.Repositories.CustomRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsersByFirstName(string firstName);
    }
}