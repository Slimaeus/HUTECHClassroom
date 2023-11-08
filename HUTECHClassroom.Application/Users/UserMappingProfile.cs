using HUTECHClassroom.Application.Users.DTOs;

namespace HUTECHClassroom.Application.Users;

public sealed class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, UserDTO>()
            .ForMember(x => x.Class, o => o.MapFrom(x => x.Class != null ? x.Class.Name : null));
    }
}