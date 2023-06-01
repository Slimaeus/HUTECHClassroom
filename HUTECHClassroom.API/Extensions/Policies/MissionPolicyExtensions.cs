using HUTECHClassroom.Domain.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class MissionPolicyExtensions
{
    public static void AddMissionPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateMissionPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.MISSION, CreateAction);
        });
        options.AddPolicy(ReadMissionPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.MISSION, ReadAction);
        });
        options.AddPolicy(UpdateMissionPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.MISSION, UpdateAction);
        });
        options.AddPolicy(DeleteMissionPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.MISSION, DeleteAction);
        });
    }
}
