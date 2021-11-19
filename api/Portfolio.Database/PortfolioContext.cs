using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Common;

namespace Portfolio.Database
{
    public class PortfolioContext : DbContext
    {
        #region Constructor

        public PortfolioContext(DbContextOptions<PortfolioContext> options)
            : base(options)
        {
        }

        #endregion

        #region Fields

        public DbSet<AboutMe> AboutMes { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<SkillGroup> SkillGroups { get; set; }

        public DbSet<EmailSettings> EmailSettings { get; set; }

        public DbSet<SeoSettings> SeoSettings { get; set; }

        public DbSet<GeneralSettings> GeneralSettings { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Used to update auditable entities.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changes = from e in ChangeTracker.Entries()
                where e.State != EntityState.Unchanged
                select e;

            foreach (var change in changes)
            {
                if (change.State == EntityState.Deleted && change.Entity is ISoftDelete softDelete)
                {
                    softDelete.IsDeleted = true;
                    change.State = EntityState.Modified;
                }

                if ((change.State == EntityState.Modified || change.State == EntityState.Added) && HasUpdatedTime(change.Entity) && change.Entity is IHasUpdatedDate updateTime)
                    updateTime.UpdatedAtUtc = DateTime.UtcNow;

                if (change.State != EntityState.Added || !HasCreationTime(change.Entity))
                    continue;

                if (change.Entity is IHasCreationDate creationTime)
                    creationTime.CreatedAtUTC = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region Utils

        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        private static bool HasUpdatedTime<TEntity>(TEntity entity)
        {
            var entityType = entity.GetType();
            return typeof(IHasUpdatedDate).IsAssignableFrom(entityType);
        }

        private static bool HasCreationTime<TEntity>(TEntity entity)
        {
            var entityType = entity.GetType();
            return typeof(IHasCreationDate).IsAssignableFrom(entityType);
        }

        private static bool HasSoftDelete<TEntity>(TEntity entity)
        {
            var entityType = entity.GetType();
            return typeof(ISoftDelete).IsAssignableFrom(entityType);
        }

        #endregion
    }
}
