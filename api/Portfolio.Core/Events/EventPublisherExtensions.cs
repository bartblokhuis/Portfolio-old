using Portfolio.Domain.Models.Common;
using System.Threading.Tasks;

namespace Portfolio.Core.Events;

/// <summary>
/// Event publisher extensions
/// </summary>
public static class EventPublisherExtensions
{
    /// <summary>
    /// Entity inserted
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="eventPublisher">Event publisher</param>
    /// <param name="entity">Entity</param>
/// <returns>A task that represents the asynchronous operation</returns>
    public static async Task EntityInsertedAsync<T, TKey>(this IEventPublisher eventPublisher, T entity) where T : class, IBaseEntity<TKey>
    {
        await eventPublisher.PublishAsync(new EntityInsertedEvent<T, TKey>(entity));
    }

    /// <summary>
    /// Entity updated
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="eventPublisher">Event publisher</param>
    /// <param name="entity">Entity</param>
/// <returns>A task that represents the asynchronous operation</returns>
    public static async Task EntityUpdatedAsync<T, TKey>(this IEventPublisher eventPublisher, T entity) where T : class, IBaseEntity<TKey>
    {
        await eventPublisher.PublishAsync(new EntityUpdatedEvent<T, TKey>(entity));
    }

    /// <summary>
    /// Entity deleted
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="eventPublisher">Event publisher</param>
    /// <param name="entity">Entity</param>
/// <returns>A task that represents the asynchronous operation</returns>
    public static async Task EntityDeletedAsync<T, TKey>(this IEventPublisher eventPublisher, T entity) where T : class, IBaseEntity<TKey>
    {
        await eventPublisher.PublishAsync(new EntityDeletedEvent<T, TKey>(entity));
    }
}
 