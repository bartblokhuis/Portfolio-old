using Portfolio.Domain.Models.Blogs;

namespace Portfolio.Services.Comments;

public  interface IBlogPostCommentService
{
    Task<Comment> GetByIdAsync(int id);

    Task<Comment> GetParentCommentAsync(Comment comment);

    Task InsertAsync(Comment comment);

    Task DeleteAsync(int id);
}
