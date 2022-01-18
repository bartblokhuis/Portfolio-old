using System.Collections.Generic;

namespace Portfolio.Domain.Dtos.Comments;
public class CommentDto
{
    #region Properties

    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Content { get; set; }

    public bool IsAuthor { get; set; }

    public ParrentCommentDto ParentComment { get; set; }

    public int? ParentCommentId { get; set; }

    public ICollection<CommentDto> Comments { get; set; }

    #endregion
}

