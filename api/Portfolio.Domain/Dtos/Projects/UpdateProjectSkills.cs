namespace Portfolio.Domain.Dtos.Projects;

public class UpdateProjectSkills
{
    #region Properties

    public int ProjectId { get; set; }

    public int[] SkillIds { get; set; }

    #endregion
}
