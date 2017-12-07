using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<User>> GetAll();
        Task<User> GetByIdAsync(Guid id);
        Task<List<User>> FindByAsync(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        Task<bool> Delete(Guid id);
        void Edit(T entity);
    }
}
