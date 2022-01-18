using AutoMapper;
using Portfolio.Domain.Dtos.Comments;
using Portfolio.Domain.Models;

namespace Portfolio.Core.AutoMapper;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CreateCommentDto, Comment>();
        CreateMap<Comment, CommentDto>();
        CreateMap<Comment, ListCommentDto>();
        CreateMap<Comment, ParrentCommentDto>();
    }
}

