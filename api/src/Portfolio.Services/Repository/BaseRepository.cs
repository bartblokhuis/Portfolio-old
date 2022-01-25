using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Caching;
using Portfolio.Core.Events;
using Portfolio.Database;
using Portfolio.Domain.Models.Common;
using System.Linq.Expressions;

namespace Portfolio.Services.Repository;

public class BaseRepository<TEntity> : BaseRepository<TEntity, int>, IBaseRepository<TEntity>
    where TEntity : class, IBaseEntity<int>
{
    public BaseRepository(PortfolioContext context, IEventPublisher eventPublisher, IStaticCacheManager staticCacheManager)
        : base(context, eventPublisher, staticCacheManager)
    {
    }
}

public class BaseRepository<TEntity, TKey> : BaseRepository<TEntity, TKey, PortfolioContext>, IBaseRepository<TEntity, TKey>
    where TEntity : class, IBaseEntity<TKey>
{
    public BaseRepository(PortfolioContext context, IEventPublisher eventPublisher, IStaticCacheManager staticCacheManager)
        : base(context, eventPublisher, staticCacheManager)
    {
    }
}

public class BaseRepository<TEntity, TKey, TDbContext> : IBaseRepository<TEntity, TKey, TDbContext>
    where TDbContext : PortfolioContext
    where TEntity : class, IBaseEntity<TKey>
{
    #region Fields

    private readonly PortfolioContext _context;
    private readonly DbSet<TEntity> _dbSet;
    private readonly IEventPublisher _eventPublisher;
    private readonly IStaticCacheManager _staticCacheManager;

    #endregion

    #region Constructor

    public BaseRepository(TDbContext context, IEventPublisher eventPublisher, IStaticCacheManager staticCacheManager)
    {
        _context = context;
        _eventPublisher = eventPublisher;
        _staticCacheManager = staticCacheManager;
        _dbSet = context.Set<TEntity>();
        Table = _dbSet;
    }

    #endregion

    #region Utils

    protected virtual IQueryable<TEntity> AddDeletedFilter(IQueryable<TEntity> query, in bool includeDeleted)
    {
        if (includeDeleted)
            return query;

        if (typeof(TEntity).GetInterface(nameof(ISoftDelete)) == null)
            return query;

        return query.OfType<ISoftDelete>().Where(entry => !entry.IsDeleted).OfType<TEntity>();
    }

    #endregion

    #region Methods

    #region Get

    protected virtual async Task<IList<TEntity>> GetEntitiesAsync(Func<Task<IList<TEntity>>> getAllAsync, Func<IStaticCacheManager, CacheKey> getCacheKey)
    {
        if (getCacheKey == null)
            return await getAllAsync();

        //caching
        var cacheKey = getCacheKey(_staticCacheManager)
                       ?? _staticCacheManager.PrepareKeyForDefaultCache(PortfolioEntityCacheDefaults<TEntity, TKey>.AllCacheKey);
        return await _staticCacheManager.GetAsync(cacheKey, getAllAsync);
    }

    public virtual async Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            Func<IStaticCacheManager, CacheKey> getCacheKey = null, bool includeDeleted = true)
    {
        async Task<IList<TEntity>> getAllAsync()
        {
            var query = AddDeletedFilter(Table, includeDeleted);
            query = query.AsNoTracking();
            query = func != null ? func(query) : query;

            return await query.ToListAsync();
        }

        return await GetEntitiesAsync(getAllAsync, getCacheKey);
    }

    public virtual async Task<IPagedList<TEntity>> GetAllPagedAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false, bool includeDeleted = true)
    {
        var query = AddDeletedFilter(Table, includeDeleted);

        query = func != null ? func(query) : query;

        return await query.ToPagedListAsync(pageIndex, pageSize, getOnlyTotalCount);
    }

    public DbSet<TEntity> Table { get; }

    public async Task<TEntity> GetByIdAsync(TKey id, string includeProperties = "", Func<IStaticCacheManager, CacheKey> getCacheKey = null)
    {
        if (id == null)
            return null;

        async Task<TEntity> getEntityAsync()
        {
            var query = _dbSet.AsTracking();

            if (includeProperties != null)
                query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.FirstOrDefaultAsync(x => x.Id.ToString() == id.ToString());
        }

        if (getCacheKey == null)
            return await getEntityAsync();

        //caching
        var cacheKey = getCacheKey(_staticCacheManager)
            ?? _staticCacheManager.PrepareKeyForDefaultCache(PortfolioEntityCacheDefaults<TEntity, TKey>.ByIdCacheKey, id);

        return await _staticCacheManager.GetAsync(cacheKey, getEntityAsync);
    }

    public async Task<IQueryable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (filter != null)
            query = query.Where(filter);

        if (includeProperties != null)
            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        if (orderBy != null)
            return orderBy(query);
        

        return query;
    }

    public Task<List<TEntity>> GetAllAsync()
    {
        return _dbSet.ToListAsync();
    }

    public async Task<TEntity> FirstAsync(Func<IStaticCacheManager, CacheKey> getCacheKey = null, bool includeDeleted = true)
    {
        async Task<TEntity> getEntityAsync()
        {
            return await _dbSet.FirstOrDefaultAsync();
        }

        if (getCacheKey == null)
            return await getEntityAsync();


        //caching
        var cacheKey = getCacheKey(_staticCacheManager);
        return await _staticCacheManager.GetAsync(cacheKey, getEntityAsync);
    }

    public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.FirstOrDefaultAsync(predicate: predicate);
    }

    #endregion

    #region Insert

    public async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChanges();

        await _eventPublisher.EntityInsertedAsync<TEntity, TKey>(entity);
    }

    public virtual async Task InsertAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await SaveChanges();

        foreach (var entity in entities)
            await _eventPublisher.EntityInsertedAsync<TEntity, TKey>(entity);
    }

    #endregion

    #region Delete

    public async Task DeleteAsync(TKey id)
    {
        var entity = await _dbSet.FindAsync(id);
        await DeleteAsync(entity);
        await SaveChanges();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _eventPublisher.EntityDeletedAsync<TEntity, TKey>(entity);

        await SaveChanges();
    }

    public async Task DeleteAsync(IList<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);

        foreach (var entity in entities)
            await _eventPublisher.EntityDeletedAsync<TEntity, TKey>(entity);

        await SaveChanges();
    }

    #endregion

    #region Update

    public async Task UpdateAsync(TEntity entity)
    {
        await _eventPublisher.EntityUpdatedAsync<TEntity, TKey>(entity);

        _dbSet.Update(entity);
        await SaveChanges();
    }

    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
            await _eventPublisher.EntityUpdatedAsync<TEntity, TKey>(entity);

        _dbSet.UpdateRange(entities);
        await SaveChanges();
    }

    #endregion

    #region Utils

    private async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public int Count()
        => _dbSet.Count();

    #endregion

    #endregion
}

