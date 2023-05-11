using HUTECHClassroom.API.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class GroupRolePolicyExtensions
{
    public static void AddGroupRolePolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(RequireLeaderGroupRolePolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleRequirement("Leader"));
        });
    }
}
