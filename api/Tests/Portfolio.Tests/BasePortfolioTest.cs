using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Core.Caching;
using Portfolio.Core.Infrastructure;
using System;

namespace Portfolio.Tests;

public abstract class BasePortfolioTest
{

    private static readonly ServiceProvider _serviceProvider;

    static BasePortfolioTest()
    {
        var services = new ServiceCollection();

        services.AddHttpClient();

        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var typeFinder = new AppDomainTypeFinder();

        services.AddSingleton<IMemoryCache>(memoryCache);
        services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
        services.AddSingleton<ILocker, MemoryCacheManager>();

        _serviceProvider = services.BuildServiceProvider();
    }

    public T GetService<T>()
    {
        try
        {
            return _serviceProvider.GetRequiredService<T>();
        }
        catch (InvalidOperationException)
        {
            return (T)EngineContext.Current.ResolveUnregistered(typeof(T));
        }
    }
}
