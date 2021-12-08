namespace Portfolio.Domain.Dtos.Skills
{
    public class UpdateSkillDto : CreateUpdateSkill
    {
        public int Id { get; set; }

        public string IconPath { get; set; }
    }
}
