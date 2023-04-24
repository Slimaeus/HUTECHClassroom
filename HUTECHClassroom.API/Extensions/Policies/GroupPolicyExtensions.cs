using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class GroupPolicyExtensions
{
    public static void AddGroupPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateGroupPolicy, policy =>
        {
            policy.RequireClaim(GroupClaimName, CreateAction);
        });
        options.AddPolicy(ReadGroupPolicy, policy =>
        {
            policy.RequireClaim(GroupClaimName, ReadAction);
        });
        options.AddPolicy(UpdateGroupPolicy, policy =>
        {
            policy.RequireClaim(GroupClaimName, UpdateAction);
        });
        options.AddPolicy(DeleteGroupPolicy, policy =>
        {
            policy.RequireClaim(GroupClaimName, DeleteAction);
        });
    }
}
