using Portfolio.Domain.Enums;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Common;

namespace Portfolio.Services.Messages;

public interface IMessageService
{
    Task<IEnumerable<Message>> GetAllAsync();

    Task<IPagedList<Message>> GetAllMessagesAsync(int pageIndex = 0, int pageSize = int.MaxValue);

    Task<Message> InsertAsync(Message model);

    Task<bool> IsAllowedAsync(string ipAddress);

    Task<Message> GetByIdAsync(int messageId);

    Task UpdateMessageStatusAsync(Message message, MessageStatus status);

    Task DeleteAsync(Message message);
}
