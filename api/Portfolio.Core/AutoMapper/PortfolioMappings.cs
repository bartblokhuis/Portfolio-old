using AutoMapper;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Dtos.Projects;
using Portfolio.Domain.Dtos.SkillGroup;
using Portfolio.Domain.Dtos.Skills;
using Portfolio.Domain.Models;

namespace Portfolio.Core.AutoMapper;

public class PortfolioMappings : Profile
{
    public PortfolioMappings()
    {
        CreateMap<AboutMe, AboutMeDto>();
        CreateMap<AboutMeDto, AboutMe>().ForMember(x => x.Id, options => options.Ignore());

        CreateMap<EmailSettings, EmailSettingsDto>();
        CreateMap<EmailSettingsDto, EmailSettings>().ForMember(x => x.Id, options => options.Ignore());

        CreateMap<SeoSettings, SeoSettingsDto>();
        CreateMap<SeoSettingsDto, SeoSettings>().ForMember(x => x.Id, options => options.Ignore());

        CreateMap<GeneralSettings, GeneralSettingsDto>();
        CreateMap<GeneralSettingsDto, GeneralSettings>().ForMember(x => x.Id, options => options.Ignore());

        CreateMap<Message, MessageDto>().ReverseMap();

        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<CreateUpdateProject, Project>();

        CreateMap<CreateSkillDto, Skill>();
        CreateMap<UpdateSkillDto, Skill>();
        CreateMap<Skill, SkillDto>();
        CreateMap<SkillDto, Skill>();

        CreateMap<CreateUpdateSkillGroupDto, SkillGroup>();
        CreateMap<SkillGroup, SkillGroupDto>();
        CreateMap<SkillGroupDto, SkillGroup>();
    }
}

