namespace HUTECHClassroom.API.Authorization.GroupRoles;

public class GroupRoleRequirement : IAuthorizationRequirement
{
    public string RoleName { get; }

    public GroupRoleRequirement(string roleName)
    {
        RoleName = roleName;
    }
}

