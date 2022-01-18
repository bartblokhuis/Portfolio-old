using Portfolio.Domain.Models;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Comments;

public  interface IBlogPostCommentService
{
    Task<Comment> GetByIdAsync(int id);

    Task CreateAsync(Comment comment);

    Task DeleteAsync(int id);
}
