using Portfolio.Domain.Dtos.Common;

namespace Portfolio.Domain.Dtos.Messages;

public record MessagesListDto : BasePagedListModel<MessageDto>
{
}
