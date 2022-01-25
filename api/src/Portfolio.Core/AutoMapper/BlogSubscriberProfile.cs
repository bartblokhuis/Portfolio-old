using AutoMapper;
using Portfolio.Domain.Dtos.BlogSubscribers;
using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Wrapper;

namespace Portfolio.Core.AutoMapper;

public class BlogSubscriberProfile : Profile
{
    public BlogSubscriberProfile()
    {
        CreateMap<BlogSubscriber, ListBlogSubscriberDto>();

        CreateMap<BlogSubscriber, BlogSubscriberDto>();
        CreateMap<CreateBlogSubscriberDto, BlogSubscriber>();

        CreateMap<ListResult<BlogSubscriber>, ListResult<ListBlogSubscriberDto>>();
    }
}