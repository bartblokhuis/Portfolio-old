using Portfolio.Core.Caching;

namespace Portfolio.Core.Services.Blogs;

public static partial class BlogPostDefaults
{
    public static string PublishedBlogPostsPrefix => "Portfolio.blog.posts.published.";

    public static string AllBlogPostsPrefix => "Portfolio.blog.posts.";

    public static CacheKey PublishedBlogPostsCacheKey => new CacheKey("Portfolio.blog.posts.published.", PublishedBlogPostsPrefix);

    public static CacheKey AllBlogPostsCacheKey => new CacheKey("Portfolio.blog.posts.", AllBlogPostsPrefix);
}
