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
        });
        options.AddPolicy(ReadMissionPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.STUDENT);
        });
        options.AddPolicy(UpdateMissionPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.STUDENT);
        });
        options.AddPolicy(DeleteMissionPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.STUDENT);
        });
    }
}
