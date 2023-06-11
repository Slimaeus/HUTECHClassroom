using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class FacultyPolicyExtensions
{
    public static void AddFacultyPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateFacultyPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
        options.AddPolicy(ReadFacultyPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateFacultyPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);

        });
        options.AddPolicy(DeleteFacultyPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
    }
}
