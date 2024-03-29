﻿using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Common;

namespace Portfolio.Services.QueuedEmails;

public interface IQueuedEmailService
{
    Task<IPagedList<QueuedEmail>> GetAllQueuedEmailsAsync(int pageIndex = 0, int pageSize = int.MaxValue);

    Task InsertAsync(QueuedEmail queuedEmail);

    Task UpdateAsync(QueuedEmail queuedEmail);

    Task DeleteAsync(QueuedEmail queuedEmail);

    Task DeleteAsync(IList<QueuedEmail> queuedEmail);

    Task<QueuedEmail> GetByIdAsync(int id);

    Task<IPagedList<QueuedEmail>> SearcAsync(string fromEmail,
            string toEmail, DateTime? createdFromUtc, DateTime? createdToUtc,
            bool loadNotSentItemsOnly, bool loadOnlyItemsToBeSent, int maxSendTries,
            bool loadNewest, int pageIndex = 0, int pageSize = int.MaxValue);

    Task DeleteAlreadySentAsync(DateTime? createdFromUtc, DateTime? createdToUtc);

    Task DeleteAllAsync();
}
