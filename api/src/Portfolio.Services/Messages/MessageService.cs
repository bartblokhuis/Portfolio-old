using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Common;
using Portfolio.Services.Repository;

namespace Portfolio.Services.Messages;

public class MessageService : IMessageService
{
    #region Fields

    private readonly IBaseRepository<Message> _messageRepository;

    #endregion

    #region Constructor

    public MessageService(IBaseRepository<Message> messageRepository)
    {
        _messageRepository = messageRepository;
    }

    #endregion

    #region Methods

    public async Task<IPagedList<Message>> GetAllMessagesAsync(int pageIndex = 0, int pageSize = int.MaxValue)
    {
        return await _messageRepository.GetAllPagedAsync(query =>
        {
            query = query.OrderByDescending(b => b.CreatedAtUTC);
            return query;

        }, pageIndex, pageSize);
    }

    public async Task<Message> InsertAsync(Message message)
    {
        await _messageRepository.InsertAsync(message);
        return message;
    }

    public async Task<IEnumerable<Message>> GetAllAsync()
    {
        var messages = await _messageRepository.GetAllAsync(includeDeleted: false);
        return messages.OrderByDescending(x => x.CreatedAtUTC);
    }

    public Task<bool> IsAllowedAsync(string ipAddress)
    {
        var minTime = DateTime.UtcNow.AddMinutes(-2);
        return _messageRepository.Table.Where(x => x.IpAddress == ipAddress).AllAsync(x => x.CreatedAtUTC <= minTime);
    }

    public Task<Message> GetByIdAsync(int messageId)
    {
        return _messageRepository.GetByIdAsync(messageId);
    }

    public Task DeleteAsync(Message message)
    {
        return _messageRepository.DeleteAsync(message);
    }

    public Task UpdateMessageStatusAsync(Message message, MessageStatus status)
    {
        message.MessageStatus = status;
        return _messageRepository.UpdateAsync(message);
    }

    #region Utils

    #endregion

    #endregion
}
