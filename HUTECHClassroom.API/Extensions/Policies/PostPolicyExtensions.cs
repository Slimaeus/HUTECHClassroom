using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class PostPolicyExtensions
{
    public static void AddPostPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreatePostPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.STUDENT);
        });
        options.AddPolicy(ReadPostPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdatePostPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.STUDENT);
        });
        options.AddPolicy(DeletePostPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.STUDENT);
        });
    }
}
