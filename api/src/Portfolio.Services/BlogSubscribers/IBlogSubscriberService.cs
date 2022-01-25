﻿using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Models.Common;

namespace Portfolio.Services.BlogSubscribers;

public interface IBlogSubscriberService
{
    Task<IPagedList<BlogSubscriber>> GetAllAsync(DateTime? createdFromUtc = null, DateTime? createdToUtc = null, bool getOnlyTotalCount = false);

    Task<BlogSubscriber> GetByIdAsync(Guid id);

    Task<bool> ExistsAsync(string email);

    Task SubscribeAsync(BlogSubscriber blogSubscriber);

    Task UnsubscribeAsync(BlogSubscriber blogSubscriber);
}
