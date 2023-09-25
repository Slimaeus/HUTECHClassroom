using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class AnswerPolicyExtensions
{
    public static void AddAnswerPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateAnswerPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.STUDENT, RoleConstants.LECTURER);

        });
        options.AddPolicy(ReadAnswerPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateAnswerPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.STUDENT, RoleConstants.LECTURER);
        });
        options.AddPolicy(DeleteAnswerPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.STUDENT, RoleConstants.LECTURER);
        });
    }
}
