using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class CommonPolicyExtensions
{
    public static void AddCommonPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(NeedRolePolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Student, RoleConstants.Lecturer, RoleConstants.Dean, RoleConstants.TrainingOffice, RoleConstants.Administrator);
        });
    }
}
