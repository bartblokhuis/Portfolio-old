namespace Portfolio.Domain.Dtos.BlogPosts;

public abstract class BaseBlogPostDto
{
    #region Properties

    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsPublished { get; set; }

    public int DisplayNumber { get; set; }

    #endregion
}

