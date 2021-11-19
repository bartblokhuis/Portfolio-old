using Portfolio.Domain.Dtos.Common;
using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain.Dtos
{
    public class SkillGroupDto : BaseDto
    {
        #region Properties

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        public int DisplayNumber { get; set; }

        public IEnumerable<Skill> Skills { get; set; }

        #endregion
    }
}
