using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class MajorPolicyExtensions
{
    public static void AddMajorPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateMajorPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(ReadMajorPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateMajorPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);

        });
        options.AddPolicy(DeleteMajorPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
    }
}
