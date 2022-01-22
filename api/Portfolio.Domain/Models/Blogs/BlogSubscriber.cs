using Portfolio.Domain.Models.Common;
using System;

namespace Portfolio.Domain.Models.Blogs;
public class BlogSubscriber : BaseEntity<Guid>, IFullyAudited, ISoftDelete
{
    #region Properties

    public string Name { get; set; }

    public string EmailAddress { get; set; }

    public DateTime CreatedAtUTC { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public bool IsDeleted { get; set; }

    #endregion
}
