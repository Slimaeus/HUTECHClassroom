using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class SubjectPolicyExtensions
{
    public static void AddSubjectPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateSubjectPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
        options.AddPolicy(ReadSubjectPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateSubjectPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);

        });
        options.AddPolicy(DeleteSubjectPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
    }
}
