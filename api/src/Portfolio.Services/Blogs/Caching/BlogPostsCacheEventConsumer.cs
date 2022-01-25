using Portfolio.Core.Caching;
using Portfolio.Domain.Models.Blogs;

namespace Portfolio.Services.Blogs.Caching;

public partial class BlogPostsCacheEventConsumer : CacheEventConsumer<BlogPost, int>
{
    protected override async Task ClearCacheAsync(BlogPost entity, EntityEventType entityEventType)
    {
        await RemoveAsync(BlogPostDefaults.AllBlogPostsCacheKey);
        await RemoveAsync(BlogPostDefaults.PublishedBlogPostsCacheKey);

        await base.ClearCacheAsync(entity, entityEventType);
    }
}