using Portfolio.Core.Caching;
using Portfolio.Core.Services.Blogs;
using Portfolio.Domain.Models.Blogs;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Comments.Caching;
public class CommentsCacheEventConsumer : CacheEventConsumer<Comment, int>
{
    protected override async Task ClearCacheAsync(Comment entity, EntityEventType entityEventType)
    {
        await RemoveAsync(BlogPostDefaults.AllBlogPostsCacheKey, entity);
        await RemoveAsync(BlogPostDefaults.PublishedBlogPostsCacheKey);

        await base.ClearCacheAsync(entity, entityEventType);
    }
}
