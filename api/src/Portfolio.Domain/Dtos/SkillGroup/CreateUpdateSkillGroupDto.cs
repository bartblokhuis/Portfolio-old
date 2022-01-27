using Portfolio.Domain.Dtos.Common;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain.Dtos.SkillGroup;

public class CreateUpdateSkillGroupDto : BaseDto
{
    #region Properties

    [Required(AllowEmptyStrings = false)]
    [MaxLength(64, ErrorMessage = "Please don't enter a title with more than 64 characters")]
    public string Title { get; set; }

    public int DisplayNumber { get; set; }

    #endregion
}
