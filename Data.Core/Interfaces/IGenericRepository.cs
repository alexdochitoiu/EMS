using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync<TOrderKey>(Func<IQueryable<T>, IQueryable<T>> load, 
            Expression<Func<T, TOrderKey>> orderBy = null);
        Task<IEnumerable<T>> GetAllAsync<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy = null);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> load);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<T> EditAsync(T entity, object key);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
