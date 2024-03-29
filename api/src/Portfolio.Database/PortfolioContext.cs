﻿using Microsoft.EntityFrameworkCore;
using Portfolio.Database.Configurations;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Models.Common;
using Portfolio.Domain.Models.Localization;
using Portfolio.Domain.Models.Settings;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Database;

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

    public DbSet<BlogPost> BlogPosts { get; set; }

    public DbSet<Picture> Pictures { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Url> Urls { get; set; }

    public DbSet<ProjectPicture> ProjectPictures { get; set; }

    public DbSet<ProjectUrls> ProjectUrls { get; set; }

    public DbSet<BlogSubscriber> BlogSubscribers { get; set; }

    public DbSet<BlogSettings> BlogSettings { get; set; }

    public DbSet<PublicSiteSettings> PublicSiteSettings { get; set; }

    public DbSet<QueuedEmail> QueuedEmails { get; set; }

    public DbSet<ScheduleTask> ScheduleTasks { get; set; }

    public DbSet<ApiSettings> ApiSettings { get; set; }

    public DbSet<MessageSettings> MessageSettings { get; set; }

    public DbSet<Language> Languages { get; set; }

    public DbSet<LocaleStringResource> LocaleStringResources { get; set; }

    #endregion

    #region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ScheduleTaskConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

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

            if (change.Entity is IHasCreationDate creationTime && creationTime.CreatedAtUTC.Year == 1)
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
