using Portfolio.Domain.Dtos.Common;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain.Dtos.SkillGroup;

public class CreateUpdateSkillGroupDto : BaseDto
{
    #region Properties

    [Required(AllowEmptyStrings = false)]
    public string Title { get; set; }

    public int DisplayNumber { get; set; }

    #endregion
}
