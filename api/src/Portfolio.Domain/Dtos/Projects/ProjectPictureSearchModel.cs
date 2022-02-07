using Portfolio.Domain.Dtos.Common;

namespace Portfolio.Domain.Dtos.Projects;

public record ProjectPictureSearchModel : BaseSearchModel
{
    public int ProjectId { get; set; }
}

