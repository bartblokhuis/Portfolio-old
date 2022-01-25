using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Caching;
using Portfolio.Database;
using Portfolio.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Common;

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

    Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            Func<IStaticCacheManager, CacheKey> getCacheKey = null, bool includeDeleted = true);

    Task<IPagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false, bool includeDeleted = true);

    Task InsertAsync(TEntity entuty);

    Task InsertAsync(IEnumerable<TEntity> entity);

    Task UpdateAsync(TEntity entity);

    Task UpdateRangeAsync(IEnumerable<TEntity> entities);

    Task DeleteAsync(TEntity entity);

    Task DeleteAsync(IList<TEntity> entities);

    Task DeleteAsync(TKey id);

    Task<TEntity> GetByIdAsync(TKey id, string includeProperties = "", Func<IStaticCacheManager, CacheKey> getCacheKey = null);

    int Count();

    Task<TEntity> FirstAsync(Func<IStaticCacheManager, CacheKey> getCacheKey = null, bool includeDeleted = true);

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
}
