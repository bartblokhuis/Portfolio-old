﻿using Portfolio.Domain.Models.Common;

namespace Portfolio.Core.Caching;

public static partial class PortfolioEntityCacheDefaults<TEntity, TKey> where TEntity : class, IBaseEntity<TKey>
{
    /// <summary>
    /// Gets an entity type name used in cache keys
    /// </summary>
    public static string EntityTypeName => typeof(TEntity).Name.ToLowerInvariant();

    /// <summary>
    /// Gets a key for caching entity by identifier
    /// </summary>
    /// <remarks>
    /// {0} : entity id
    /// </remarks>
    public static CacheKey ByIdCacheKey => new CacheKey($"Portfolio.{EntityTypeName}.byid.{{0}}", ByIdPrefix, Prefix);

    /// <summary>
    /// Gets a key for caching entities by identifiers
    /// </summary>
    /// <remarks>
    /// {0} : entity ids
    /// </remarks>
    public static CacheKey ByIdsCacheKey => new CacheKey($"Portfolio.{EntityTypeName}.byids.{{0}}", ByIdsPrefix, Prefix);

    /// <summary>
    /// Gets a key for caching all entities
    /// </summary>
    public static CacheKey AllCacheKey => new CacheKey($"Portfolio.{EntityTypeName}.all.", AllPrefix, Prefix);

    /// <summary>
    /// Gets a key pattern to clear cache
    /// </summary>
    public static string Prefix => $"Portfolio.{EntityTypeName}.";

    /// <summary>
    /// Gets a key pattern to clear cache
    /// </summary>
    public static string ByIdPrefix => $"Portfolio.{EntityTypeName}.byid.";

    /// <summary>
    /// Gets a key pattern to clear cache
    /// </summary>
    public static string ByIdsPrefix => $"Portfolio.{EntityTypeName}.byids.";

    /// <summary>
    /// Gets a key pattern to clear cache
    /// </summary>
    public static string AllPrefix => $"Portfolio.{EntityTypeName}.all.";
}
