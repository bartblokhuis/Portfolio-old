using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portfolio.Core.Infrastructure;

/// <summary>
/// Represents Nop engine
/// </summary>
public class PortfolioEngine : IEngine
{
    #region Utilities

    /// <summary>
    /// Get IServiceProvider
    /// </summary>
    /// <returns>IServiceProvider</returns>
    protected IServiceProvider GetServiceProvider(IServiceScope scope = null)
    {
        if (scope == null)
        {
            var accessor = ServiceProvider?.GetService<IHttpContextAccessor>();
            var context = accessor?.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }
        return scope.ServiceProvider;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Add and configure services
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var typeFinder = new WebAppTypeFinder();

        //register engine
        services.AddSingleton<IEngine>(this);

        //register type finder
        services.AddSingleton<ITypeFinder>(typeFinder);

        //event consumers
        var consumers = typeFinder.FindClassesOfType(typeof(IConsumer<>)).ToList();
        foreach (var consumer in consumers)
            foreach (var findInterface in consumer.FindInterfaces((type, criteria) =>
            {
                var isMatch = type.IsGenericType && ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
                return isMatch;
            }, typeof(IConsumer<>)))
                services.AddScoped(findInterface, consumer);
    }

    /// <summary>
    /// Configure HTTP request pipeline
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void ConfigureRequestPipeline(IApplicationBuilder application)
    {
        ServiceProvider = application.ApplicationServices;
    }

    /// <summary>
    /// Resolve dependency
    /// </summary>
    /// <param name="scope">Scope</param>
    /// <typeparam name="T">Type of resolved service</typeparam>
    /// <returns>Resolved service</returns>
    public T Resolve<T>(IServiceScope scope = null) where T : class
    {
        return (T)Resolve(typeof(T), scope);
    }

    /// <summary>
    /// Resolve dependency
    /// </summary>
    /// <param name="type">Type of resolved service</param>
    /// <param name="scope">Scope</param>
    /// <returns>Resolved service</returns>
    public object Resolve(Type type, IServiceScope scope = null)
    {
        return GetServiceProvider(scope)?.GetService(type);
    }

    /// <summary>
    /// Resolve dependencies
    /// </summary>
    /// <typeparam name="T">Type of resolved services</typeparam>
    /// <returns>Collection of resolved services</returns>
    public virtual IEnumerable<T> ResolveAll<T>()
    {
        return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
    }

    /// <summary>
    /// Resolve unregistered service
    /// </summary>
    /// <param name="type">Type of service</param>
    /// <returns>Resolved service</returns>
    public virtual object ResolveUnregistered(Type type)
    {
        Exception innerException = null;
        foreach (var constructor in type.GetConstructors())
        {
            try
            {
                //try to resolve constructor parameters
                var parameters = constructor.GetParameters().Select(parameter =>
                {
                    var service = Resolve(parameter.ParameterType);
                    if (service == null)
                        throw new Exception("Unknown dependency");
                    return service;
                });

                //all is ok, so create instance
                return Activator.CreateInstance(type, parameters.ToArray());
            }
            catch (Exception ex)
            {
                innerException = ex;
            }
        }

        throw new Exception("No constructor was found that had all the dependencies satisfied.", innerException);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Service provider
    /// </summary>
    public virtual IServiceProvider ServiceProvider { get; protected set; }

    #endregion
}