using System.Collections.Generic;

namespace Portfolio.Domain.Dtos.Comments;

public class ListCommentDto
{
    #region Properties

    public int Id { get; set; }

    public string Name { get; set; }

    public string Content { get; set; }

    public bool IsAuthor { get; set; }

    public ICollection<ListCommentDto> Comments { get; set; }

    #endregion
   
}
