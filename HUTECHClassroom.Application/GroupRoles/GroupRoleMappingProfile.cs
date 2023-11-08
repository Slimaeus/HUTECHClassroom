using HUTECHClassroom.Application.GroupRoles.DTOs;

namespace HUTECHClassroom.Application.GroupRoles;

public sealed class GroupRoleMappingProfile : Profile
{
    public GroupRoleMappingProfile()
    {
        CreateMap<GroupRole, GroupRoleDTO>();
    }
}
