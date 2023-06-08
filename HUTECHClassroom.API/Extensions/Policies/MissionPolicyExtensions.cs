using HUTECHClassroom.API.Authorization.Missions;
using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class MissionPolicyExtensions
{
    public static void AddMissionPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateMissionPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleFromMissionRequirement(GroupRoleConstants.LEADER));
        });
        options.AddPolicy(ReadMissionPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateMissionPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleFromMissionRequirement(GroupRoleConstants.LEADER));
        });
        options.AddPolicy(DeleteMissionPolicy, policy =>
        {
            policy.AddRequirements(new GroupRoleFromMissionRequirement(GroupRoleConstants.LEADER));
        });
    }
}
