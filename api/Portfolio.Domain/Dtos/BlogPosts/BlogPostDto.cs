using Portfolio.Domain.Dtos.Comments;
using Portfolio.Domain.Models;
using System;
using System.Collections.Generic;

namespace Portfolio.Domain.Dtos.BlogPosts;

public class BlogPostDto : BaseBlogPostDto
{
    #region Properties

    public string Content { get; set; }

    public int Id { get; set; }

    public string MetaTitle { get; set; }

    public string MetaDescription { get; set; }

    public DateTime CreatedAtUTC { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public Picture? Thumbnail { get; set; }

    public int? ThumbnailId { get; set; }

    public Picture? BannerPicture { get; set; }

    public int? BannerPictureId { get; set; }

    public ICollection<ListCommentDto> Comments { get; set; }

    #endregion
}

