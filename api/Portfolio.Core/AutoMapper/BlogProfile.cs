using AutoMapper;
using Portfolio.Domain.Dtos.Blogs;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;

namespace Portfolio.Core.AutoMapper;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<Blog, ListBlogDto>().ReverseMap();
        CreateMap<Blog, BlogDto>();

        CreateMap<CreateBlogDto, Blog>();
        CreateMap<UpdateBlogDto, Blog>();

        CreateMap<ListResult<Blog>, ListResult<ListBlogDto>>();
    }
}
