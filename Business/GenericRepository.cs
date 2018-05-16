using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseService _databaseService;

        protected GenericRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public virtual async Task<List<T>> GetAll(Func<IQueryable<T>, IQueryable<T>> func)
        {
            var loadedData = func(_databaseService.Set<T>());
            return await loadedData.ToListAsync();
        }

        public virtual async Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate, 
            Func<IQueryable<T>, IQueryable<T>> func)
        {
            var loadedData = func(_databaseService.Set<T>());
            return await loadedData.Where(predicate).ToListAsync();
        }

        public virtual async Task<T> Add(T entity)
        {
            await _databaseService.Set<T>().AddAsync(entity);
            await _databaseService.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> Edit(T entity, object key)
        {
            if (entity == null)
                return null;
            var exist = await _databaseService.Set<T>().FindAsync(key);
            if (exist == null) return null;
            _databaseService.Entry(exist).CurrentValues.SetValues(entity);
            await _databaseService.SaveChangesAsync();
            return exist;
        }

        public virtual async Task<int> Delete(T entity)
        {
            _databaseService.Set<T>().Remove(entity);
            return await _databaseService.SaveChangesAsync();
        }
    }
}
