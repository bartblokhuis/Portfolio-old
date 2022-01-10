using Portfolio.Domain.Models;
using System;

namespace Portfolio.Domain.Dtos.Blogs;

public  class ListBlogDto : BaseBlogDto
{
    #region Properties

    public int Id { get; set; }

    public DateTime CreatedAtUTC { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public Picture? Thumbnail { get; set; }

    public int? ThumbnailId { get; set; }

    #endregion
}
