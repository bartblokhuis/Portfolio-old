using Portfolio.Core.Caching;
using Portfolio.Domain.Models;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Blogs.Caching;

public partial class BlogPostsCacheEventConsumer : CacheEventConsumer<BlogPost, int>
{
    protected override async Task ClearCacheAsync(BlogPost entity, EntityEventType entityEventType)
    {
        await RemoveByPrefixAsync(BlogPostDefaults.PublishedBlogPostsPrefix, entity);
        await RemoveByPrefixAsync(BlogPostDefaults.AllBlogPostsPrefix, entity);

        await base.ClearCacheAsync(entity, entityEventType);
    }
}