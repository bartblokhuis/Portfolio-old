using Portfolio.Domain.Enums;
using Portfolio.Domain.Models;

namespace Portfolio.Services.Messages;

public interface IMessageService
{
    Task<IEnumerable<Message>> GetAllAsync();

    Task<Message> InsertAsync(Message model);

    Task<bool> IsAllowedAsync(string ipAddress);

    Task<Message> GetByIdAsync(int messageId);

    Task UpdateMessageStatusAsync(Message message, MessageStatus status);

    Task DeleteAsync(Message message);
}
