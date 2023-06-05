using HUTECHClassroom.API.Authorization.GroupRoles;
using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class ProjectPolicyExtensions
{
    public static void AddProjectPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateProjectPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
        });
        options.AddPolicy(ReadProjectPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateProjectPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
        });
        options.AddPolicy(DeleteProjectPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
        });
    }
}
