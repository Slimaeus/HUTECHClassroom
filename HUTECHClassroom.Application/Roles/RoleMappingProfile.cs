using HUTECHClassroom.Application.Roles.DTOs;

namespace HUTECHClassroom.Application.Roles;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<ApplicationRole, RoleDTO>();
    }
}
