using HUTECHClassroom.Domain.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class GroupPolicyExtensions
{
    public static void AddGroupPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateGroupPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.Group, CreateAction);
        });
        options.AddPolicy(ReadGroupPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.Group, ReadAction);
        });
        options.AddPolicy(UpdateGroupPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.Group, UpdateAction);
        });
        options.AddPolicy(DeleteGroupPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.Group, DeleteAction);
        });
    }
}
