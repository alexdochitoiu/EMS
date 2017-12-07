using System.Linq;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> GetByFirstName(string firstName);
        IQueryable<User> GetByLastName(string lastName);
        IQueryable<User> GetByAge(int age);
    }
}
