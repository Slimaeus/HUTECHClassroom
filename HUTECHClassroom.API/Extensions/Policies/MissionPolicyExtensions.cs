using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class MissionPolicyExtensions
{
    public static void AddMissionPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateMissionPolicy, policy =>
        {
            //policy.AddRequirements(new GroupRoleFromMissionRequirement(GroupRoleConstants.LEADER));
            policy.RequireRole(RoleConstants.STUDENT, RoleConstants.LECTURER);

        });
        options.AddPolicy(ReadMissionPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateMissionPolicy, policy =>
        {
            //policy.AddRequirements(new GroupRoleFromMissionRequirement(GroupRoleConstants.LEADER));
            policy.RequireRole(RoleConstants.STUDENT, RoleConstants.LECTURER);

        });
        options.AddPolicy(DeleteMissionPolicy, policy =>
        {
            //policy.AddRequirements(new GroupRoleFromMissionRequirement(GroupRoleConstants.LEADER));
            policy.RequireRole(RoleConstants.STUDENT, RoleConstants.LECTURER);

        });
    }
}
