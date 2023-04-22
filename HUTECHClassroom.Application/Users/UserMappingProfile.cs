using HUTECHClassroom.Application.Users.DTOs;

namespace HUTECHClassroom.Application.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, UserDTO>();
    }
}