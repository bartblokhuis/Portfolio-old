using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain.Dtos;

public abstract class CreateUpdateSkill
{
    #region Properties

    [Required(AllowEmptyStrings = false)]
    [MaxLength(64)]
    public string Name { get; set; }

    [Required]
    public int DisplayNumber { get; set; }

    [Required]
    public int SkillGroupId { get; set; }

    #endregion
}
