
using AutoMapper;
using Portfolio.Domain.Dtos.QueuedEmails;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;

namespace Portfolio.Core.AutoMapper;

public class QueuedEmailProfile : Profile
{
    public QueuedEmailProfile()
    {
        CreateMap<QueuedEmail, ListQueuedEmailDto>();
        CreateMap<ListResult<QueuedEmail>, ListResult<ListQueuedEmailDto>>();
        CreateMap<UpdateQueuedEmailDto, QueuedEmail>().ReverseMap();
    }
}
