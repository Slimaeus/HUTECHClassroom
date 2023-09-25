using HUTECHClassroom.API.Extensions.Policies;

namespace HUTECHClassroom.API.Extensions;

public static class AuthorizationOptionsExtensions
{
    public static void AddEntityPolicies(this AuthorizationOptions options)
    {
        options.AddFacultyPolicies();
        options.AddMissionPolicies();
        options.AddProjectPolicies();
        options.AddGroupPolicies();
        options.AddGroupRolePolicies();
        options.AddClassroomPolicies();
        options.AddExercisePolicies();
        options.AddMajorPolicies();
        options.AddSubjectPolicies();
        options.AddPostPolicies();
        options.AddCommentPolicies();
        options.AddAnswerPolicies();
        options.AddCommonPolicies();
    }
}
