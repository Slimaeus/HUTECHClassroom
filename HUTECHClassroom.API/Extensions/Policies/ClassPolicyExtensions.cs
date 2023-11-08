using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class ClassPolicyExtensions
{
    public static void AddClassPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateClassPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(ReadClassPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateClassPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);

        });
        options.AddPolicy(DeleteClassPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
    }
}
