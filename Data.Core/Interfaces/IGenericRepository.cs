using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll(Func<IQueryable<T>, IQueryable<T>> func);
        Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> func);
        Task<T> Add(T entity);
        Task<T> Edit(T entity, object key);
        Task<int> Delete(T entity);
    }
}
