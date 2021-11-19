using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;

namespace Portfolio.Core.Interfaces.Common
{
    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, int>
        where TEntity : class
    {

    }

    public interface IBaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey, PortfolioContext>
        where TEntity : class
    {

    }

    public interface IBaseRepository<TEntity, TKey, TDbContext>
        where TEntity : class
    {
        public DbSet<TEntity> Table { get; }

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<List<TEntity>> GetAllAsync();

        Task InsertAsync(TEntity entuty);

        Task InsertAsync(IEnumerable<TEntity> entity);

        Task UpdateAsync(TEntity entity);

        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);

        Task DeleteAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TKey id);

        Task<TEntity> GetByIdAsync(TKey id);

        int Count();

        Task<TEntity> FirstAsync();

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
