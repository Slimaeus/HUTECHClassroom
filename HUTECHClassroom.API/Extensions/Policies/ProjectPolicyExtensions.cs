using HUTECHClassroom.Domain.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class ProjectPolicyExtensions
{
    public static void AddProjectPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateProjectPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.PROJECT, CreateAction);
        });
        options.AddPolicy(ReadProjectPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.PROJECT, ReadAction);
        });
        options.AddPolicy(UpdateProjectPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.PROJECT, UpdateAction);
        });
        options.AddPolicy(DeleteProjectPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.PROJECT, DeleteAction);
        });
    }
}
