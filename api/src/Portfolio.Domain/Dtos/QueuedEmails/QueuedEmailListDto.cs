using Portfolio.Domain.Dtos.Common;

namespace Portfolio.Domain.Dtos.QueuedEmails;

public record QueuedEmailListDto : BasePagedListModel<ListQueuedEmailDto>
{
}

