using Portfolio.Domain.Enums;
using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Messages;

public interface IMessageService
{
    Task<IEnumerable<Message>> GetAllAsync();

    Task<Message> InsertAsync(Message model);

    Task<bool> IsAllowedAsync(string ipAddress);

    Task<Message> GetByIdAsync(int messageId);

    Task UpdateMessageStatusAsync(Message message, MessageStatus status);

    Task DeleteAsync(Message message);
}
