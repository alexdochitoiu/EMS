using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<User>> GetAll();
        Task<List<User>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<User> GetByIdAsync(Guid id);
        Task Add(T entity);
        Task Edit(T entity);
        Task<bool> Delete(Guid id);
    }
}
