using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models;

public class ProjectUrls : BaseEntity
{

    #region Constructors

    public ProjectUrls()
    {

    }

    public ProjectUrls(int projectId, int urlId)
    {
        ProjectId = projectId;
        UrlId = urlId;
    }

    #endregion
    #region Properties

    public Project Project { get; set; }

    public int ProjectId { get; set; }

    public Url Url { get; set; }

    public int UrlId { get; set; }

    #endregion
}

