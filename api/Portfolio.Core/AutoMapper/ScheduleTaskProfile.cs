using AutoMapper;
using Portfolio.Domain.Dtos.ScheduleTasks;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;

namespace Portfolio.Core.AutoMapper;

public class ScheduleTaskProfile : Profile
{
    public ScheduleTaskProfile()
    {
        CreateMap<ScheduleTask, ListScheduleTaskDto>().ReverseMap();

        CreateMap<CreateScheduleTaskDto, ScheduleTask>();
        CreateMap<UpdateScheduleTaskDto, ScheduleTask>();

        CreateMap<ListResult<ScheduleTask>, ListResult<ListScheduleTaskDto>>();
    }
}
