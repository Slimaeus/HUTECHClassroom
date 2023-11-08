using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.API.Extensions.Policies;

public static class CommentPolicyExtensions
{
    public static void AddCommentPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(CreateCommentPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Student, RoleConstants.Lecturer, RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
        options.AddPolicy(ReadCommentPolicy, policy =>
        {
            policy.RequireAuthenticatedUser();
        });
        options.AddPolicy(UpdateCommentPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Student, RoleConstants.Lecturer, RoleConstants.Dean, RoleConstants.TrainingOffice);

        });
        options.AddPolicy(DeleteCommentPolicy, policy =>
        {
            policy.RequireRole(RoleConstants.Student, RoleConstants.Lecturer, RoleConstants.Dean, RoleConstants.TrainingOffice);
        });
    }
}
