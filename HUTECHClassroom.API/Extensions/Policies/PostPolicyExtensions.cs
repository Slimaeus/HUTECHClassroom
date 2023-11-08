using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class PostPolicyExtensions
{
    public static void AddPostPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreatePostPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer, RoleConstants.Student);
        });
        options.AddPolicy(ReadPostPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdatePostPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer, RoleConstants.Student);
        });
        options.AddPolicy(DeletePostPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer, RoleConstants.Student);
        });
    }
}
