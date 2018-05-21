using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        protected GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> load)
        {
            var loadedData = load(_dbContext.Set<T>());
            return await loadedData.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() => 
            await _dbContext.Set<T>().ToListAsync();
        

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, 
            Func<IQueryable<T>, IQueryable<T>> load)
        {
            var loadedData = load(_dbContext.Set<T>());
            return await loadedData.Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _dbContext.Set<T>().Where(predicate).ToListAsync();

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public virtual async Task<T> EditAsync(T entity, object key)
        {
            if (entity == null)
                return null;
            var exist = await _dbContext.Set<T>().FindAsync(key);
            if (exist == null) return null;
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return exist;
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }
    }
}
