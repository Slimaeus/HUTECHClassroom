using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class ExercisePolicyExtensions
{
    public static void AddExercisePolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateExercisePolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer);
        });
        options.AddPolicy(ReadExercisePolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateExercisePolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer);
        });
        options.AddPolicy(DeleteExercisePolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer);
        });
    }
}
