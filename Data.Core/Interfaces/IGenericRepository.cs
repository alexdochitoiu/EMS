using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task<T> Edit(T entity, object key);
        Task<int> Delete(T entity);
    }
}
