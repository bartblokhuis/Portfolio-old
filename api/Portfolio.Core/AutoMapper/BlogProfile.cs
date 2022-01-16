using AutoMapper;
using Portfolio.Domain.Dtos.BlogPosts;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;

namespace Portfolio.Core.AutoMapper;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<BlogPost, ListBlogPostDto>().ReverseMap();
        CreateMap<BlogPost, BlogPostDto>();

        CreateMap<CreateBlogPostDto, BlogPost>();
        CreateMap<UpdateBlogPostDto, BlogPost>();

        CreateMap<ListResult<BlogPost>, ListResult<ListBlogPostDto>>();
    }
}
