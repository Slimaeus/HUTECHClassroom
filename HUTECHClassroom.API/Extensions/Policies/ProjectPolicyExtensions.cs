using HUTECHClassroom.API.Authorization.Projects;
using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class ProjectPolicyExtensions
{
    public static void AddProjectPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateProjectPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleFromProjectRequirement(GroupRoleConstants.LEADER));
        });
        options.AddPolicy(ReadProjectPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();

        });
        options.AddPolicy(UpdateProjectPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleFromProjectRequirement(GroupRoleConstants.LEADER));
        });
        options.AddPolicy(DeleteProjectPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleFromProjectRequirement(GroupRoleConstants.LEADER));
        });
    }
}
