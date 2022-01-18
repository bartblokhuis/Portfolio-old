namespace Portfolio.Domain.Dtos.Comments;

public partial class CreateCommentDto
{
    #region Properties

    public string Name { get; set; }

    public string Email { get; set; }

    public string Content { get; set; }

    public int? BlogPostId { get; set; }

    public int? ParentCommentId { get; set; }

    #endregion
}

