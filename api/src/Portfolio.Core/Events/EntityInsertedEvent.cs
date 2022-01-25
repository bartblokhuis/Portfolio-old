using Portfolio.Domain.Models.Common;

namespace Portfolio.Core.Events;

/// <summary>
/// A container for entities that have been inserted.
/// </summary>
/// <typeparam name="T"></typeparam>
public class EntityInsertedEvent<T, TKey> where T : class, IBaseEntity<TKey>
{
    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="entity">Entity</param>
    public EntityInsertedEvent(T entity)
    {
        Entity = entity;
    }

    /// <summary>
    /// Entity
    /// </summary>
    public T Entity { get; }

}
