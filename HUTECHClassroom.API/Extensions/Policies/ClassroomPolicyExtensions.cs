using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class ClassroomPolicyExtensions
{
    public static void AddClassroomPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateClassroomPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(ReadClassroomPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateClassroomPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(DeleteClassroomPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(AddClassroomUserPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(RemoveClassroomUserPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
    }
}
