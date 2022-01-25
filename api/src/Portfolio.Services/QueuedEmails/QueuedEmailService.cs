using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Common;
using Portfolio.Services.Repository;

namespace Portfolio.Services.QueuedEmails;

public class QueuedEmailService : IQueuedEmailService
{
    #region Fields

    private readonly IBaseRepository<QueuedEmail> _queuedEmailRepository;

    #endregion

    #region Constructor

    public QueuedEmailService(IBaseRepository<QueuedEmail> queuedEmailRepository)
    {
        _queuedEmailRepository = queuedEmailRepository;
    }

    #endregion

    #region Methods

    public async Task InsertAsync(QueuedEmail queuedEmail)
    {
        await _queuedEmailRepository.InsertAsync(queuedEmail);
    }

    public async Task UpdateAsync(QueuedEmail queuedEmail)
    {
        await _queuedEmailRepository.UpdateAsync(queuedEmail);
    }

    public async Task DeleteAsync(QueuedEmail queuedEmail)
    {
        await _queuedEmailRepository.DeleteAsync(queuedEmail);
    }

    public async Task DeleteAsync(IList<QueuedEmail> queuedEmails)
    {
        await _queuedEmailRepository.DeleteAsync(queuedEmails);
    }

    public async Task<QueuedEmail> GetByIdAsync(int id)
    {
        return await _queuedEmailRepository.GetByIdAsync(id);
    }

    public async Task<IPagedList<QueuedEmail>> SearcAsync(string fromEmail, string toEmail, DateTime? createdFromUtc, DateTime? createdToUtc, bool loadNotSentItemsOnly, bool loadOnlyItemsToBeSent, int maxSendTries, bool loadNewest, int pageIndex = 0, int pageSize = int.MaxValue)
    {
        fromEmail = (fromEmail ?? string.Empty).Trim();
        toEmail = (toEmail ?? string.Empty).Trim();

        var queuedEmails = await _queuedEmailRepository.GetAllPagedAsync(query =>
        {
            if (!string.IsNullOrEmpty(fromEmail))
                query = query.Where(qe => qe.From.Contains(fromEmail));
            if (!string.IsNullOrEmpty(toEmail))
                query = query.Where(qe => qe.To.Contains(toEmail));
            if (createdFromUtc.HasValue)
                query = query.Where(qe => qe.CreatedAtUTC >= createdFromUtc);
            if (createdToUtc.HasValue)
                query = query.Where(qe => qe.CreatedAtUTC <= createdToUtc);
            if (loadNotSentItemsOnly)
                query = query.Where(qe => !qe.SentOnUtc.HasValue);

            query = query.Where(qe => qe.SentTries < maxSendTries);
            if(loadNewest)
                //load the newest records
                query.OrderByDescending(qe => qe.CreatedAtUTC);

            return query;
        });

        return queuedEmails;
    }

    public async Task DeleteAlreadySentAsync(DateTime? createdFromUtc, DateTime? createdToUtc)
    {
        var alreadySentEmails = await _queuedEmailRepository.GetAllAsync(query =>
        {
            query.Where(qe => qe.SentOnUtc.HasValue);

            if (createdFromUtc.HasValue)
                query = query.Where(qe => qe.CreatedAtUTC >= createdFromUtc);
            if (createdToUtc.HasValue)
                query = query.Where(qe => qe.CreatedAtUTC <= createdToUtc);

            return query;
        });

        var emails = alreadySentEmails.ToArray();
        await DeleteAsync(emails);

    }

    public async Task DeleteAllAsync()
    {
        var emails = await _queuedEmailRepository.GetAllAsync();
        await DeleteAsync(emails);
    }

    #endregion
}
