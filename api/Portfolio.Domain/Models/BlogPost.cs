using Portfolio.Domain.Models.Common;
using System;
using System.Collections.Generic;

namespace Portfolio.Domain.Models;

public class BlogPost : BaseEntity, IHasDisplayNumber, IFullyAudited
{
    #region Properties

    public string Title { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    public bool IsPublished { get; set; }

    public int DisplayNumber { get; set; }

    public string MetaTitle { get; set; }

    public string MetaDescription { get; set; }

    public DateTime CreatedAtUTC { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public Picture? Thumbnail { get; set; }

    public int? ThumbnailId { get; set; }

    public Picture? BannerPicture { get; set; }

    public int? BannerPictureId { get; set; }

    public ICollection<Comment> Comments { get; set; }

    #endregion
}

