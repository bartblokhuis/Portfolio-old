using Portfolio.Domain.Models.Blogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.BlogSubscribers;

public interface IBlogSubscriberService
{
    Task<IEnumerable<BlogSubscriber>> GetAllAsync();

    Task<BlogSubscriber> GetByIdAsync(Guid id);

    Task<bool> Exists(string email);

    Task SubscribeAsync(BlogSubscriber blogSubscriber);

    Task UnsubscribeAsync(BlogSubscriber blogSubscriber);
}
