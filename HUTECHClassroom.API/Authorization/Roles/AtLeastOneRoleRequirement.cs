using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Authorization.Roles;

public class AtLeastOneRoleRequirement : IAuthorizationRequirement
{
    public string[] Roles { get; }

    public AtLeastOneRoleRequirement(params string[] roles)
    {
        Roles = roles;
    }
}
