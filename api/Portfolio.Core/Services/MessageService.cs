using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Common;

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

        public async Task<Message> Create(Message message)
        {
            await _messageRepository.InsertAsync(message);
            return message;
        }

        public async Task<IQueryable<Message>> Get()
        {
            var messages = await _messageRepository.GetAsync((message) => message.IsDeleted == false);
            return messages.OrderByDescending(x => x.CreatedAtUTC);
        }

        public Task<bool> IsAllowed(string ipAddress)
        {
            var minTime = DateTime.UtcNow.AddMinutes(-2);
            return _messageRepository.Table.Where(x => x.IpAddress == ipAddress).AllAsync(x => x.CreatedAtUTC <= minTime);
        }

        public Task<Message> GetById(int messageId)
        {
            return _messageRepository.GetByIdAsync(messageId);
        }

        public Task Delete(Message message)
        {
            return _messageRepository.DeleteAsync(message);
        }

        public Task UpdateMessageStatus(Message message, MessageStatus status)
        {
            message.MessageStatus = status;
            return _messageRepository.UpdateAsync(message);
        }

        #region Utils

        #endregion

        #endregion
    }
