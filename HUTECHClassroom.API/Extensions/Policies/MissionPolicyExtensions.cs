using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class MissionPolicyExtensions
{
    public static void AddMissionPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateMissionPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.STUDENT);
            //policy.RequireClaim(ApplicationClaimTypes.MISSION, CreateAction);
        });
        options.AddPolicy(ReadMissionPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.STUDENT);
            //policy.RequireClaim(ApplicationClaimTypes.MISSION, ReadAction);
        });
        options.AddPolicy(UpdateMissionPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.STUDENT);
            //policy.RequireClaim(ApplicationClaimTypes.MISSION, UpdateAction);
        });
        options.AddPolicy(DeleteMissionPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.STUDENT);
            //policy.RequireClaim(ApplicationClaimTypes.MISSION, DeleteAction);
        });
    }
}
