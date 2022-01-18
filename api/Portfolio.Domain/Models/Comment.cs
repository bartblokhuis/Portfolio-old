using Portfolio.Domain.Models.Common;
using System;
using System.Collections.Generic;

namespace Portfolio.Domain.Models;

public class Comment : BaseEntity, IFullyAudited
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Content { get; set; }

    public bool IsAuthor { get; set; }

    public DateTime CreatedAtUTC { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public BlogPost BlogPost { get; set; }

    public int? BlogPostId { get; set; }

    public Comment ParentComment { get; set; }

    public int? ParentCommentId { get; set; }

    public ICollection<Comment> Comments { get; set; }
}
