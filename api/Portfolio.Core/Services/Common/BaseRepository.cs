using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Database;
using Portfolio.Domain.Models.Common;

namespace Portfolio.Core.Services.Common
{
    public class BaseRepository<TEntity> : BaseRepository<TEntity, int>, IBaseRepository<TEntity>
        where TEntity : class, IBaseEntity<int>
    {
        public BaseRepository(PortfolioContext context, IMapper mapper)
            : base(context, mapper)
        {
        }
    }

    public class BaseRepository<TEntity, TKey> : BaseRepository<TEntity, TKey, PortfolioContext>, IBaseRepository<TEntity, TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        public BaseRepository(PortfolioContext context, IMapper mapper)
            : base(context, mapper)
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
        
        #endregion

        #region Constructor

        public BaseRepository(TDbContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            Table = _dbSet;
        }

        #endregion

        #region Methods

        #region Get

        public DbSet<TEntity> Table { get; }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
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
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public Task<TEntity> FirstAsync()
        {
            return _dbSet.FirstOrDefaultAsync();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefaultAsync(predicate: predicate);
        }

        #endregion

        #region Create

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChanges();
        }

        public virtual async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await SaveChanges();
        }

        #endregion

        #region Delete

        public async Task DeleteAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            await DeleteAsync(entity);
            await SaveChanges();
        }

        public Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return SaveChanges();
        }

        public Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return SaveChanges();
        }

        #endregion

        #region Update

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChanges();
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
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
}
