using HUTECHClassroom.API.Authorization.GroupRoles;

namespace HUTECHClassroom.API.Authorization.Projects;

public sealed class GroupRoleFromProjectRequirement : GroupRoleRequirement
{
    public GroupRoleFromProjectRequirement(string roleName) : base(roleName)
    {
    }
}
