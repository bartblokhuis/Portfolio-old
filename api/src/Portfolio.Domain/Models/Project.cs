using Portfolio.Domain.Models.Common;
using System.Collections.Generic;

namespace Portfolio.Domain.Models;

public class Project : BaseEntity, IHasDisplayNumber
{
    #region Properties

    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsPublished { get; set; }

    public int DisplayNumber { get; set; }

    public ICollection<Skill> Skills { get; set; }

    public ICollection<ProjectUrls> ProjectUrls { get; set; }

    public ICollection<ProjectPicture> ProjectPictures { get; set; }

    #endregion
}
