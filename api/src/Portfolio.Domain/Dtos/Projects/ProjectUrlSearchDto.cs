
using Portfolio.Domain.Dtos.Common;

namespace Portfolio.Domain.Dtos.Projects;

public record ProjectUrlSearchDto : BaseSearchModel
{
    public int ProjectId { get; set; }
}

