using AutoMapper;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Dtos.Projects;
using Portfolio.Domain.Dtos.SkillGroup;
using Portfolio.Domain.Dtos.Skills;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;

namespace Portfolio.Core.AutoMapper;

public class PortfolioMappings : Profile
{
    public PortfolioMappings()
    {
        CreateMap<AboutMe, AboutMeDto>();
        CreateMap<AboutMeDto, AboutMe>().ForMember(x => x.Id, options => options.Ignore());

        CreateMap<Message, MessageDto>().ReverseMap();

        CreateMap<CreateSkillDto, Skill>();
        CreateMap<UpdateSkillDto, Skill>();
        CreateMap<Skill, SkillDto>();
        CreateMap<SkillDto, Skill>();

        CreateMap<CreateUpdateSkillGroupDto, SkillGroup>();
        CreateMap<SkillGroup, SkillGroupDto>();
        CreateMap<SkillGroupDto, SkillGroup>();

        CreateMap<ListResult<SkillGroup>, ListResult<SkillGroupDto>>();
        CreateMap<ListResult<Message>, ListResult<MessageDto>>();
        CreateMap<ListResult<Project>, ListResult<ProjectDto>>();
        CreateMap<ListResult<Skill>, ListResult<SkillDto>>();
    }
}

