using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HUTECHClassroom.API.Authorization;

public class GroupRoleAuthorizationHandler : AuthorizationHandler<GroupRoleRequirement>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GroupRoleAuthorizationHandler(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, GroupRoleRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId != null)
        {
            var user = await _userManager.Users
                .Include(x => x.GroupUsers)
                .ThenInclude(x => x.Group)
                .SingleOrDefaultAsync(x => x.Id.ToString() == userId);
            var groupId = GetGroupIdFromRoute();

            Console.WriteLine(groupId);

            if (groupId != null && user != null && IsUserInGroupWithRole(user, groupId.Value, requirement.RoleName))
            {
                context.Succeed(requirement);
            }
        }
    }

    private bool IsUserInGroupWithRole(ApplicationUser user, Guid groupId, string roleName)
    {
        var groupUser = user.GroupUsers.FirstOrDefault(gu => (gu.GroupId == groupId && gu.GroupRole?.Name == roleName) || (gu.GroupId == groupId && gu.Group.LeaderId == user.Id));
        return groupUser != null;
    }

    private Guid? GetGroupIdFromRoute()
    {
        var routeData = _httpContextAccessor.HttpContext?.GetRouteData();
        if (routeData != null && routeData.Values.TryGetValue("groupId", out var idValue))
        {
            if (Guid.TryParse(idValue.ToString(), out var groupId))
            {
                return groupId;
            }
        }
        return null;
    }
}

