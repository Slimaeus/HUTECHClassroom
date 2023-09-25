using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class ClassroomPolicyExtensions
{
    public static void AddClassroomPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateClassroomPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
        options.AddPolicy(ReadClassroomPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateClassroomPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
        options.AddPolicy(DeleteClassroomPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
        options.AddPolicy(AddClassroomUserPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
        options.AddPolicy(RemoveClassroomUserPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
    }
}
