using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models;
public class ProjectPicture : BaseEntity, IHasDisplayNumber
{
    #region Constructors

    public ProjectPicture()
    {

    }

    public ProjectPicture(Project project, Picture picture)
    {
        Project = project;
        ProjectId = project.Id;

        Picture = picture;
        PictureId = picture.Id;
    }

    #endregion

    #region Properties

    public int DisplayNumber { get; set; }

    public Project Project { get; set; }

    public int ProjectId { get; set; }

    public Picture Picture { get; set; }

    public int PictureId { get; set; }

    #endregion
}
