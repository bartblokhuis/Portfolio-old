using AutoMapper;
using Portfolio.Domain.Dtos.Languages;
using Portfolio.Domain.Models.Localization;
using Portfolio.Domain.Wrapper;

namespace Portfolio.Core.AutoMapper;

public class LanguageProfile : Profile
{
    public LanguageProfile()
    {
        CreateMap<Language, LanguageDto>().ReverseMap();
        CreateMap<Language, LanguageListDto>();

        CreateMap<LanguageCreateDto, Language>();
        CreateMap<LanguageUpdateDto, Language>();

        CreateMap<ListResult<Language>, ListResult<LanguageListDto>>();
    }
}
