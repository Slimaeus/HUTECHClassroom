using HUTECHClassroom.Application.GroupRoles.DTOs;

namespace HUTECHClassroom.Application.GroupRoles;

public class GroupRoleMappingProfile : Profile
{
    public GroupRoleMappingProfile()
    {
        CreateMap<GroupRole, GroupRoleDTO>();
    }
}
