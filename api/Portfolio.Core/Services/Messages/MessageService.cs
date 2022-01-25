using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Messages;

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
