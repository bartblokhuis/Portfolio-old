using System;

namespace Portfolio.Domain.Dtos.Blogs;

public  class UpdateBlogDto : BaseBlogDto
{
    #region Properties

    public int Id { get; set; }

    public DateTime CreatedAtUTC { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public string Content { get; set; }

    #endregion
}

