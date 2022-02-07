using Portfolio.Domain.Dtos.Common;
using Portfolio.Domain.Models;

namespace Portfolio.Domain.Dtos.Pictures;

public record PictureListDto : BasePagedListModel<Picture>
{
}
