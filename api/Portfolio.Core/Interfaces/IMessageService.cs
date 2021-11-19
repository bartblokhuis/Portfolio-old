using Portfolio.Domain.Dtos;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> Get();

        Task<Message> Create(Message model);

        Task<bool> IsAllowed(string ipAddress);

        Task<Message> GetById(int messageId);

        Task UpdateMessageStatus(Message message, MessageStatus status);

        Task Delete(Message message);
    }
}
