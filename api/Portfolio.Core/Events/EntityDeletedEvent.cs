using Portfolio.Domain.Models.Common;

namespace Portfolio.Core.Events;

/// <summary>
/// A container for passing entities that have been deleted. This is not used for entities that are deleted logically via a bit column.
/// </summary>
/// <typeparam name="T"></typeparam>
public class EntityDeletedEvent<T, TKey> where T : class, IBaseEntity<TKey>
{
    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="entity">Entity</param>
    public EntityDeletedEvent(T entity)
    {
        Entity = entity;
    }

    /// <summary>
    /// Entity
    /// </summary>
    public T Entity { get; }
}

