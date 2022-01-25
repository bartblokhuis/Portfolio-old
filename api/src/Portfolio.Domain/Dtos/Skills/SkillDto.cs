using Portfolio.Domain.Dtos.Common;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain.Dtos;

public class SkillDto : BaseDto
{
    #region Properties

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }

    public string IconPath { get; set; }

    [Required]
    public int DisplayNumber { get; set; }

    [Required]
    public int SkillGroupId { get; set; }

    public SkillGroupDto SkillGroup { get; set; }

    #endregion
}
