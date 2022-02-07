using Portfolio.Domain.Dtos.Common;

namespace Portfolio.Domain.Dtos.BlogPosts;

public record BlogPostListModel : BasePagedListModel<ListBlogPostDto>
{
}
