using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class CommonPolicyExtensions
{
    public static void AddCommonPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(NeedRolePolicy, policy =>
        {
            policy.RequireRole(RoleConstants.STUDENT, RoleConstants.LECTURER, RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE, RoleConstants.ADMIN);
        });
    }
}
