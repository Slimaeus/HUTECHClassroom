using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class ProjectPolicyExtensions
{
    public static void AddProjectPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateProjectPolicy, policy =>
        {
            policy.RequireClaim(ProjectClaimName, CreateAction);
        });
        options.AddPolicy(ReadProjectPolicy, policy =>
        {
            policy.RequireClaim(ProjectClaimName, ReadAction);
        });
        options.AddPolicy(UpdateProjectPolicy, policy =>
        {
            policy.RequireClaim(ProjectClaimName, UpdateAction);
        });
        options.AddPolicy(DeleteProjectPolicy, policy =>
        {
            policy.RequireClaim(ProjectClaimName, DeleteAction);
        });
    }
}
