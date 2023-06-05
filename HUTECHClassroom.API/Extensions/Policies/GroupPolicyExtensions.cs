using HUTECHClassroom.API.Authorization.GroupRoles;
using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class GroupPolicyExtensions
{
    public static void AddGroupPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateGroupPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
        });
        options.AddPolicy(ReadGroupPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateGroupPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
        });
        options.AddPolicy(DeleteGroupPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
        });
    }
}
