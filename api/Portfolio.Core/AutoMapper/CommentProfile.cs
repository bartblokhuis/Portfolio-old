using AutoMapper;
using Portfolio.Domain.Dtos.Comments;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Wrapper;

namespace Portfolio.Core.AutoMapper;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CreateCommentDto, Comment>();
        CreateMap<Comment, CommentDto>();
        CreateMap<Comment, ListCommentDto>();
        CreateMap<Comment, ParrentCommentDto>();
        CreateMap<ListResult<Comment>, ListResult<ListCommentDto>>();
    }
}

