using Portfolio.Domain.Models.Common;
using System.Collections.Generic;

namespace Portfolio.Domain.Models;

public class SkillGroup : BaseEntity, IHasDisplayNumber
{
    #region Properties

    public string Title { get; set; }

    public int DisplayNumber { get; set; }

    public IEnumerable<Skill> Skills { get; set; }

    #endregion
}
