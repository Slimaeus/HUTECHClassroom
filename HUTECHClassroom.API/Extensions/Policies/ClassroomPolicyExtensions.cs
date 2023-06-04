using HUTECHClassroom.Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class ClassroomPolicyExtensions
{
    public static void AddClassroomPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateClassroomPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.STUDENT);
            //policy.RequireClaim(ApplicationClaimTypes.CLASSROOM, CreateAction);
        });
        options.AddPolicy(ReadClassroomPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.STUDENT);
            //policy.RequireClaim(ApplicationClaimTypes.CLASSROOM, ReadAction);
        });
        options.AddPolicy(UpdateClassroomPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.STUDENT);
            //policy.RequireClaim(ApplicationClaimTypes.CLASSROOM, UpdateAction);
        });
        options.AddPolicy(DeleteClassroomPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.LECTURER, RoleConstants.STUDENT);
            //policy.RequireClaim(ApplicationClaimTypes.CLASSROOM, DeleteAction);
        });
    }
}
