using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class MajorPolicyExtensions
{
    public static void AddMajorPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateMajorPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
        options.AddPolicy(ReadMajorPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateMajorPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);

        });
        options.AddPolicy(DeleteMajorPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
    }
}
