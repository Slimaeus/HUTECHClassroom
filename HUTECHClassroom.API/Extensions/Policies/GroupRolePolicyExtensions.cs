using HUTECHClassroom.API.Authorization.GroupRoles;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class GroupRolePolicyExtensions
{
    public static void AddGroupRolePolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(RequireLeaderGroupRolePolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
        });
    }
}
