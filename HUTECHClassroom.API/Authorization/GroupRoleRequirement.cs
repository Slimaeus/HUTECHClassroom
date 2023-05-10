using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Authorization;

public class GroupRoleRequirement : IAuthorizationRequirement
{
    public string RoleName { get; }

    public GroupRoleRequirement(string roleName)
    {
        RoleName = roleName;
    }
}

