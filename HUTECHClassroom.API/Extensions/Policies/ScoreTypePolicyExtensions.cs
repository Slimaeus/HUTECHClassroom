using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class ScoreTypePolicyExtensions
{
    public static void AddScoreTypePolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateScoreTypePolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer);
        });
        options.AddPolicy(ReadScoreTypePolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateScoreTypePolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer);
        });
        options.AddPolicy(DeleteScoreTypePolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer);
        });
    }
}
