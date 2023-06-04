using HUTECHClassroom.API.Extensions.Policies;
using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Extensions;

public static class AuthorizationOptionsExtensions
{
    public static void AddEntityPolicies(this AuthorizationOptions options)
    {
        options.AddMissionPolicies();
        options.AddProjectPolicies();
        options.AddGroupPolicies();
        options.AddGroupRolePolicies();
        options.AddClassroomPolicies();
    }
}
