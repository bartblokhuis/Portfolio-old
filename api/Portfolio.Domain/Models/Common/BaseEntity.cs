using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain.Models.Common;

public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
{
    #region Properties

    [Key]
    public virtual TKey Id { get; set; }

    #endregion

}

public abstract class BaseEntity : BaseEntity<int>, IBaseEntity
{
}