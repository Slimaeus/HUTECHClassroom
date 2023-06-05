using HUTECHClassroom.API.Authorization.Roles;
using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class SubjectPolicyExtensions
{
    public static void AddSubjectPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateSubjectPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));
        });
        options.AddPolicy(ReadSubjectPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateSubjectPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));

        });
        options.AddPolicy(DeleteSubjectPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));
        });
    }
}
