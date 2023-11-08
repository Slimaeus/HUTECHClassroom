using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class GroupPolicyExtensions
{
    public static void AddGroupPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateGroupPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer, RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(ReadGroupPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateGroupPolicy, policy =>
        {
            //policy.AddRequirements(new GroupRoleRequirement(GroupRoleConstants.LEADER));
            policy.RequireRole(RoleConstants.Student, RoleConstants.Lecturer, RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(DeleteGroupPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer, RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(AddGroupUserPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer, RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(RemoveGroupUserPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer, RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(AddGroupLeaderPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer, RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(RemoveGroupLeaderPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Lecturer, RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
    }
}
