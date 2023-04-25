using HUTECHClassroom.Domain.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class MissionPolicyExtensions
{
    public static void AddMissionPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateMissionPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.Mission, CreateAction);
        });
        options.AddPolicy(ReadMissionPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.Mission, ReadAction);
        });
        options.AddPolicy(UpdateMissionPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.Mission, UpdateAction);
        });
        options.AddPolicy(DeleteMissionPolicy, policy =>
        {
            policy.RequireClaim(ApplicationClaimTypes.Mission, DeleteAction);
        });
    }
}
