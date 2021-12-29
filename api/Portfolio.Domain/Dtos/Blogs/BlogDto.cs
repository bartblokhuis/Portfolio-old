using System;

namespace Portfolio.Domain.Dtos.Blogs;

public class BlogDto : BaseBlogDto
{
    #region Properties

    public string Content { get; set; }

    public int Id { get; set; }

    public DateTime CreatedAtUTC { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    #endregion
}

