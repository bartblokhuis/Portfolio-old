﻿using Portfolio.Domain.Models.Blogs;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Comments;

public  interface IBlogPostCommentService
{
    Task<Comment> GetByIdAsync(int id);

    Task<Comment> GetParentCommentAsync(Comment comment);

    Task InsertAsync(Comment comment);

    Task DeleteAsync(int id);
}
