using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Portfolio.Core.Services;

public class CacheService
{
    #region Properties

    private readonly IMemoryCache _memoryCache;

    #endregion

    #region Constructor

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    #endregion

    #region Methods

    public T Get<T>(string cacheKey)
    {
        _memoryCache.TryGetValue(cacheKey, out T result);
        return result;
    }

    public void Set<T>(string cacheKey, T data, MemoryCacheEntryOptions options = null)
    {
        if (options == null)
        {
            options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddDays(365),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromDays(365),
                Size = 1024
            };
        }

        _memoryCache.Set(cacheKey, data, options);
    }

    #endregion

}

