using HUTECHClassroom.Application.Account.DTOs;

namespace HUTECHClassroom.Application.Account;

public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        CreateMap<ApplicationUser, AccountDTO>()
            .ForMember(x => x.Roles, (config) => config.MapFrom(u => u.ApplicationUserRoles.Select(ur => ur.Role.Name)));

        CreateMap<Faculty, UserFacultyDTO>();
    }
}
