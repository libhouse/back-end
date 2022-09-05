using LibHouse.Business.Entities.Shared;
using LibHouse.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibHouse.Data.Repositories.Shared
{
    public class EntityTypeRepository<T> : IEntityRepository<T> where T : Entity
    {
        protected readonly DbSet<T> _dbSet;

        public EntityTypeRepository(LibHouseContext context)
        {
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.CountAsync(expression);
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstAsync(expression);
        }

        public async Task<List<T>> GetAsync(
            Expression<Func<T, bool>> expression = null, 
            int? skip = null,
            int? take = null)
        {
            var query = _dbSet.AsQueryable<T>();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Projection> GetProjectionAsync<Projection>(
            Expression<Func<T, bool>> expression, 
            Expression<Func<T, Projection>> projection)
        {
            return await _dbSet.Where(expression).Select(projection).FirstOrDefaultAsync();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}