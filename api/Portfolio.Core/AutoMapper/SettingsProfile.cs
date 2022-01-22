using AutoMapper;
using Portfolio.Domain.Dtos;
using Portfolio.Domain.Dtos.Settings;
using Portfolio.Domain.Models.Settings;

namespace Portfolio.Core.AutoMapper;

public class SettingsProfile : Profile
{
    #region Constructor

    public SettingsProfile()
    {
        CreateMap<EmailSettings, EmailSettingsDto>();
        CreateMap<EmailSettingsDto, EmailSettings>().ForMember(x => x.Id, options => options.Ignore());

        CreateMap<SeoSettings, SeoSettingsDto>();
        CreateMap<SeoSettingsDto, SeoSettings>().ForMember(x => x.Id, options => options.Ignore());

        CreateMap<GeneralSettings, GeneralSettingsDto>();
        CreateMap<GeneralSettingsDto, GeneralSettings>().ForMember(x => x.Id, options => options.Ignore());

        CreateMap<PublicSiteSettingsDto, PublicSiteSettings>().ForMember(x => x.Id, options => options.Ignore()).ReverseMap();
        CreateMap<BlogSettingsDto, BlogSettings>().ForMember(x => x.Id, options => options.Ignore()).ReverseMap();
    }

    #endregion
}
