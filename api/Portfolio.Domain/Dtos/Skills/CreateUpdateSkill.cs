using Portfolio.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain.Dtos
{
    public class CreateUpdateSkill
    {
        #region Properties

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string IconPath { get; set; }

        [Required]
        public int DisplayNumber { get; set; }

        [Required]
        public int SkillGroupId { get; set; }

        #endregion
    }
}
