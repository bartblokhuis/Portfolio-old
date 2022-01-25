using Portfolio.Services.Common;
using Portfolio.Domain.Models.Blogs;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Services.Repository;

namespace Portfolio.Services.Comments;

public class BlogPostCommentService : IBlogPostCommentService
{
    #region Fields

    private readonly IBaseRepository<Comment> _commentRepository;

    #endregion

    #region Constructor

    public BlogPostCommentService(IBaseRepository<Comment> commentRepository)
    {
        _commentRepository = commentRepository;
    }

    #endregion

    #region Methods

    public Task<Comment> GetByIdAsync(int id)
    {
        return _commentRepository.GetByIdAsync(id);
    }

    public async Task<Comment> GetParentCommentAsync(Comment comment)
    {
        if (comment == null || !comment.ParentCommentId.HasValue)
            return null;

        var parentComment = await _commentRepository.GetAllAsync(query =>
        {
            query = query.Where(cm => cm.Id == comment.ParentCommentId);
            return query;
        });

        return parentComment == null ? null : parentComment.FirstOrDefault();
    }

    public Task InsertAsync(Comment comment)
    {
        return _commentRepository.InsertAsync(comment);
    }

    public Task DeleteAsync(int id)
    {
        return _commentRepository.DeleteAsync(id);
    }

    #endregion
}
