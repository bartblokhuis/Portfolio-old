namespace Portfolio.Domain.Dtos.BlogPosts;

public class CreateUpdateBlogPostDto : BaseBlogPostDto
{
    #region Properties

    public string Content { get; set; }

    public int? ThumbnailId { get; set; }

    public int? BannerPictureId { get; set; }

    public string MetaTitle { get; set; }

    public string MetaDescription { get; set; }

    #endregion
}
