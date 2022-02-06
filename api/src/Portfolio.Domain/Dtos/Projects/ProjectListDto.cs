using Portfolio.Domain.Dtos.Common;

namespace Portfolio.Domain.Dtos.Projects;

public record ProjectListDto : BasePagedListModel<ProjectDto>
{
}
