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
            .BeforeMap((s, d) => d.Urls = s.ProjectUrls?.Select(x => x.Url))
            .BeforeMap((s, d) => d.Pictures = s.ProjectPictures?.Select(x =>
                new ProjectPictureDto() { AltAttribute = x.Picture.AltAttribute, 
                    DisplayNumber = x.DisplayNumber, MimeType = x.Picture.MimeType, 
                    Path = x.Picture.Path, PictureId = x.PictureId, TitleAttribute = x.Picture.TitleAttribute}));

        CreateMap<CreateUpdateProject, Project>();

        CreateMap<CreateProjectUrlDto, Url>().ForMember(x => x.Id, options => options.Ignore());
    }
}
