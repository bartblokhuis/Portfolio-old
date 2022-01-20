using AutoMapper;
using Portfolio.Domain.Dtos.Projects;
using Portfolio.Domain.Models;
using Portfolio.Domain.Wrapper;
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
        CreateMap<ProjectPicture, ProjectPictureDto>()
            .BeforeMap((s, d) => d.AltAttribute = s.Picture?.AltAttribute)
            .BeforeMap((s, d) => d.TitleAttribute = s.Picture?.TitleAttribute)
            .BeforeMap((s, d) => d.MimeType = s.Picture?.MimeType)
            .BeforeMap((s, d) => d.Path = s.Picture?.Path);

        CreateMap<ListResult<ProjectPicture>, ListResult<ProjectPictureDto>>();
    }
}
