using Portfolio.Domain.Models.Common;
using System;

namespace Portfolio.Domain.Models;

public class Blog : BaseEntity, IHasDisplayNumber, IFullyAudited
{
    #region Properties

    public string Title { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    public bool IsPublished { get; set; }

    public int DisplayNumber { get; set; }

    public DateTime CreatedAtUTC { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    #endregion
}

