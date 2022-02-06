using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Models.Common;
using Portfolio.Services.Repository;

namespace Portfolio.Services.BlogSubscribers;

public class BlogSubscriberService : IBlogSubscriberService
{
    #region Fields

    private readonly IBaseRepository<BlogSubscriber, Guid> _blogSubscriberRepository;

    #endregion

    #region Constructor

    public BlogSubscriberService(IBaseRepository<BlogSubscriber, Guid> blogSubscriberRepository)
    {
        _blogSubscriberRepository = blogSubscriberRepository;
    }

    #endregion

    #region Methods

    public async Task<IPagedList<BlogSubscriber>> GetAllAsync(DateTime? createdFromUtc = null, DateTime? createdToUtc = null, bool getOnlyTotalCount = false)
    {
        return await _blogSubscriberRepository.GetAllPagedAsync(query =>
        {
            if (createdFromUtc.HasValue)
                query = query.Where(c => createdFromUtc.Value <= c.CreatedAtUTC);
            if (createdToUtc.HasValue)
                query = query.Where(c => createdToUtc.Value >= c.CreatedAtUTC);

            query = query.Where(c => !c.IsDeleted);

            return query;
        }, getOnlyTotalCount: getOnlyTotalCount);
    }

    public async Task<IPagedList<BlogSubscriber>> GetAllSubscribersAsync(int pageIndex = 0, int pageSize = int.MaxValue)
    {
        return await _blogSubscriberRepository.GetAllPagedAsync(query =>
        {
            query = query.OrderByDescending(b => b.CreatedAtUTC);
            return query;

        }, pageIndex, pageSize);
    }

    public async Task<BlogSubscriber> GetByIdAsync(Guid id)
    {
        var blogSubscribers = await _blogSubscriberRepository.GetAllAsync(query => query.Where(x => x.IsDeleted == false && x.Id == id));
        return blogSubscribers == null || !blogSubscribers.Any() ? null : blogSubscribers.First();
    }

    public async Task<bool> ExistsAsync(string email)
    {
        var blogSubscribers = await _blogSubscriberRepository.GetAllAsync(query => query.Where(x => x.IsDeleted == false && x.EmailAddress.ToLower() == email.ToLower()));
        return blogSubscribers != null && blogSubscribers.Any();
    }

    public Task SubscribeAsync(BlogSubscriber blogSubscriber)
    {
        return _blogSubscriberRepository.InsertAsync(blogSubscriber);
    }

    public Task UnsubscribeAsync(BlogSubscriber blogSubscriber)
    {
        return _blogSubscriberRepository.DeleteAsync(blogSubscriber);
    }

    #endregion
}
