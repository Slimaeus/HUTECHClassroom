using HUTECHClassroom.Application.Account.DTOs;

namespace HUTECHClassroom.Application.Account;

public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        CreateMap<ApplicationUser, AccountDTO>()
            .ForMember(x => x.Class, (config) => config.MapFrom(u => u.Class.Name))
            .ForMember(x => x.Roles, (config) => config.MapFrom(u => u.ApplicationUserRoles.Select(ur => ur.Role.Name)))
            .ForMember(x => x.AvatarUrl, (config) => config.MapFrom(u => u.Avatar != null ? u.Avatar.Url : null));

        CreateMap<Faculty, UserFacultyDTO>();
    }
}
