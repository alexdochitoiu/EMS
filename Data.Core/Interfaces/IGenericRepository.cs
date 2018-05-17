using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Func<IQueryable<T>, IQueryable<T>> load);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> load);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task<T> Edit(T entity, object key);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
