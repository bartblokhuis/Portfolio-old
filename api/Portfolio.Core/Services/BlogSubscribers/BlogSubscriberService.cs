using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.BlogSubscribers;

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

    public async Task<IEnumerable<BlogSubscriber>> GetAllAsync()
    {
        return await _blogSubscriberRepository.GetAllAsync(query => query.Where(x => x.IsDeleted == false).OrderBy(x => x.CreatedAtUTC));
    }

    public async Task<BlogSubscriber> GetByIdAsync(Guid id)
    {
        var blogSubscribers = await _blogSubscriberRepository.GetAllAsync(query => query.Where(x => x.IsDeleted == false && x.Id == id));
        return blogSubscribers == null || !blogSubscribers.Any() ? null : blogSubscribers.First();
    }

    public async Task<bool> Exists(string email)
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
