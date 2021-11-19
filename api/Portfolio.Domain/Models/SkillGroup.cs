using System.Collections;
using System.Collections.Generic;
using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models
{
    public class SkillGroup : BaseEntity, IHasDisplayNumber
    {
        #region Properties

        public string Title { get; set; }

        public int DisplayNumber { get; set; }

        public IEnumerable<Skill> Skills { get; set; }

        #endregion
    }
}
