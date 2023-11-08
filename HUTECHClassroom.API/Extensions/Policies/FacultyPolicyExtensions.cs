using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class FacultyPolicyExtensions
{
    public static void AddFacultyPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateFacultyPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(ReadFacultyPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateFacultyPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);

        });
        options.AddPolicy(DeleteFacultyPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
    }
}
