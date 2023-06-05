using Microsoft.AspNetCore.Authorization;

namespace HUTECHClassroom.API.Authorization.Roles;

public class AtLeastOneRoleAuthorizationHandler : AuthorizationHandler<AtLeastOneRoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AtLeastOneRoleRequirement requirement)
    {
        if (requirement.Roles.Any(context.User.IsInRole))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
