using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class MissionPolicyExtensions
{
    public static void AddMissionPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateMissionPolicy, policy =>
        {
            policy.RequireClaim(MissionClaimName, CreateAction);
        });
        options.AddPolicy(ReadMissionPolicy, policy =>
        {
            policy.RequireClaim(MissionClaimName, ReadAction);
        });
        options.AddPolicy(UpdateMissionPolicy, policy =>
        {
            policy.RequireClaim(MissionClaimName, UpdateAction);
        });
        options.AddPolicy(DeleteMissionPolicy, policy =>
        {
            policy.RequireClaim(MissionClaimName, DeleteAction);
        });
    }
}
