using AutoMapper;
using Portfolio.Domain.Dtos.Projects;
using Portfolio.Domain.Models;
using System.Linq;

namespace Portfolio.Core.AutoMapper;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDto>()
            .BeforeMap((s, d) => d.Urls = s.ProjectUrls?.Select(x => x.Url));

        CreateMap<CreateUpdateProject, Project>();
    }
}
