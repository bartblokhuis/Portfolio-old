namespace Portfolio.Domain.Dtos.Comments;

public class ParrentCommentDto
{
    #region Properties

    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Content { get; set; }

    public int ThumbsUp { get; set; }

    public int ThumbsDown { get; set; }

    public bool IsAuthor { get; set; }

    #endregion
}
