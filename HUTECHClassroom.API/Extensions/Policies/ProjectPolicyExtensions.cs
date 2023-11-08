using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class ProjectPolicyExtensions
{
    public static void AddProjectPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateProjectPolicy, policy =>
        {
            //policy.AddRequirements(new GroupRoleFromProjectRequirement(GroupRoleConstants.LEADER));
            policy.RequireRole(RoleConstants.Student, RoleConstants.Lecturer);
        });
        options.AddPolicy(ReadProjectPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();

        });
        options.AddPolicy(UpdateProjectPolicy, policy =>
        {
            //policy.AddRequirements(new GroupRoleFromProjectRequirement(GroupRoleConstants.LEADER));
            policy.RequireRole(RoleConstants.Student, RoleConstants.Lecturer);
        });
        options.AddPolicy(DeleteProjectPolicy, policy =>
        {
            //policy.AddRequirements(new GroupRoleFromProjectRequirement(GroupRoleConstants.LEADER));
            policy.RequireRole(RoleConstants.Student, RoleConstants.Lecturer);
        });
    }
}
