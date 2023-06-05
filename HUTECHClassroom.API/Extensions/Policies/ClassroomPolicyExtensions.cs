using HUTECHClassroom.API.Authorization.Roles;
using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class ClassroomPolicyExtensions
{
    public static void AddClassroomPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateClassroomPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));
        });
        options.AddPolicy(ReadClassroomPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateClassroomPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));

        });
        options.AddPolicy(DeleteClassroomPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));
        });
        options.AddPolicy(AddClassroomUserPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));
        });
        options.AddPolicy(RemoveClassroomUserPolicy, policy =>
        {
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE));
        });
    }
}
