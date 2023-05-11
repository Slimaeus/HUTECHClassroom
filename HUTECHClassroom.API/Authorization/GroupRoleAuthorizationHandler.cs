using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HUTECHClassroom.API.Authorization;

public class GroupRoleAuthorizationHandler : AuthorizationHandler<GroupRoleRequirement>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GroupRoleAuthorizationHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, GroupRoleRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId != null)
        {
            var user = await _dbContext.Users
                .Include(x => x.GroupUsers)
                .ThenInclude(x => x.Group)
                .Include(x => x.GroupUsers)
                .ThenInclude(x => x.GroupRole)
                .SingleOrDefaultAsync(x => x.Id.ToString() == userId);
            var groupId = GetGroupIdFromRoute();

            if (groupId != null && user != null && IsUserInGroupWithRole(user, groupId.Value, requirement.RoleName))
            {
                context.Succeed(requirement);
            }
        }
    }

    private static bool IsUserInGroupWithRole(ApplicationUser user, Guid groupId, string roleName)
    {
        Console.WriteLine(user.Id);
        Console.WriteLine(roleName);
        var groupUser = user.GroupUsers.FirstOrDefault(gu => (gu.UserId == user.Id && gu.GroupId == groupId && gu.GroupRole?.Name == roleName));
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

