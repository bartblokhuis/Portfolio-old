using Portfolio.Domain.Models.Blogs;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Comments;

public  interface IBlogPostCommentService
{
    Task<Comment> GetByIdAsync(int id);

    Task<Comment> GetParentComment(Comment comment);

    Task CreateAsync(Comment comment);

    Task DeleteAsync(int id);
}
