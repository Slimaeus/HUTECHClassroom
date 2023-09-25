using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class GroupPolicyExtensions
{
    public static void AddGroupPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateGroupPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
        options.AddPolicy(ReadGroupPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateGroupPolicy, policy =>
        {
            //policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
            policy.RequireRole(RoleConstants.STUDENT, RoleConstants.LECTURER, RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
        options.AddPolicy(DeleteGroupPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
        options.AddPolicy(AddGroupUserPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
        options.AddPolicy(RemoveGroupUserPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
        options.AddPolicy(AddGroupLeaderPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
        options.AddPolicy(RemoveGroupLeaderPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.DEAN, RoleConstants.TRAINING_OFFICE);
        });
    }
}
