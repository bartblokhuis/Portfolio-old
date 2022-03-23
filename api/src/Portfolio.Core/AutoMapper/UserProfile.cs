using AutoMapper;
using Portfolio.Domain.Dtos.Authentication;
using Portfolio.Domain.Models.Authentication;

namespace Portfolio.Core.AutoMapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserPreferences, UserPreferencesDto>();
        CreateMap<ApplicationUser, ApplicationUserDto>();
    }
}