using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibHouse.Business.Entities.Shared
{
    public interface IEntityRepository<T> where T : Entity
    {
        Task AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<T> FirstAsync(Expression<Func<T, bool>> expression);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> expression = null, int? skip = null, int? take = null);
        Task<Projection> GetProjectionAsync<Projection>(Expression<Func<T, bool>> expression, Expression<Func<T, Projection>> projection);
    }
}