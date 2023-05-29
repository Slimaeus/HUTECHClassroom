using HUTECHClassroom.Application.Account.DTOs;

namespace HUTECHClassroom.Application.Account;

public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        CreateMap<ApplicationUser, AccountDTO>()
            .ForMember(x => x.Roles, (config) => config.MapFrom(x => x.ApplicationUserRoles.Select(x => x.Role.Name)))
            .ForMember(x => x.Faculty, (config) => config.MapFrom(x => x.Faculty));

        CreateMap<Faculty, UserFacultyDTO>();
    }
}
