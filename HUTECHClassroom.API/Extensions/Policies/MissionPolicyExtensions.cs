using HUTECHClassroom.API.Authorization.Roles;
using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class MissionPolicyExtensions
{
    public static void AddMissionPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateMissionPolicy, policy =>
        {
            //policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.LECTURER, RoleConstants.STUDENT));
        });
        options.AddPolicy(ReadMissionPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateMissionPolicy, policy =>
        {
            //policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.LECTURER, RoleConstants.STUDENT));
        });
        options.AddPolicy(DeleteMissionPolicy, policy =>
        {
            //policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
            policy.AddRequirements(new AtLeastOneRoleRequirement(RoleConstants.LECTURER, RoleConstants.STUDENT));
        });
    }
}
