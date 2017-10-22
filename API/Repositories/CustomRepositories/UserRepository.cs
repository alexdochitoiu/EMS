using System.Collections.Generic;
using System.Linq;
using API.Models;
using API.Models.Entities;

namespace API.Repositories.CustomRepositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) 
            : base(context) { }

        public IEnumerable<User> GetUsersByFirstName(string firstName)
        {
            return ApplicationContext.Users.ToList().Where(u => u.FirstName.Equals(firstName));
        }

        public ApplicationContext ApplicationContext => Context as ApplicationContext;
    }
}
