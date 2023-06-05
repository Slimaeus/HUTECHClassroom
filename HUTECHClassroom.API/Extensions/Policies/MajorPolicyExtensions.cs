using HUTECHClassroom.API.Authorization.Roles;
using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class MajorPolicyExtensions
{
    public static void AddMajorPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateMajorPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));
        });
        options.AddPolicy(ReadMajorPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateMajorPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));

        });
        options.AddPolicy(DeleteMajorPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));
        });
    }
}
