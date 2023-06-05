using HUTECHClassroom.API.Authorization.Roles;
using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class FacultyPolicyExtensions
{
    public static void AddFacultyPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateFacultyPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));
        });
        options.AddPolicy(ReadFacultyPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateFacultyPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));

        });
        options.AddPolicy(DeleteFacultyPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));
        });
    }
}
