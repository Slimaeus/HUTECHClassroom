using HUTECHClassroom.API.Authorization;
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
            //policy.RequireClaim(ApplicationClaimTypes.GROUP, CreateAction);
        });
        options.AddPolicy(ReadGroupPolicy, policy =>
        {
            //policy.RequireClaim(ApplicationClaimTypes.GROUP, ReadAction);
        });
        options.AddPolicy(UpdateGroupPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
            //policy.RequireClaim(ApplicationClaimTypes.GROUP, UpdateAction);
        });
        options.AddPolicy(DeleteGroupPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
            //policy.RequireClaim(ApplicationClaimTypes.GROUP, DeleteAction);
        });
    }
}
