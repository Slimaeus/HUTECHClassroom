using HUTECHClassroom.API.Authorization.GroupRoles;

namespace HUTECHClassroom.API.Authorization.Missions;

public class GroupRoleFromMissionRequirement : GroupRoleRequirement
{
    public GroupRoleFromMissionRequirement(string roleName) : base(roleName)
    {
    }
}
