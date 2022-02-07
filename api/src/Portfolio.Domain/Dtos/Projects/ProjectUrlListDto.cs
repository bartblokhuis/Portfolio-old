using Portfolio.Domain.Dtos.Common;
using Portfolio.Domain.Models;

namespace Portfolio.Domain.Dtos.Projects;

public record ProjectUrlListDto : BasePagedListModel<Url>
{
}
