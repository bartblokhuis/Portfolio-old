using System;

namespace Portfolio.Domain.Dtos.Blogs;

public  class UpdateBlogDto : BaseBlogDto
{
    #region Properties

    public int Id { get; set; }

    public string Content { get; set; }

    public string MetaTitle { get; set; }

    public string MetaDescription { get; set; }

    public int? ThumbnailId { get; set; }

    public int? BannerPictureId { get; set; }

    #endregion
}

