using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.API.Authorization.GroupRoles;

public class GroupRoleAuthorizationHandler : AuthorizationHandler<GroupRoleRequirement>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserAccessor _userAccessor;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GroupRoleAuthorizationHandler(ApplicationDbContext dbContext, IUserAccessor userAccessor, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, GroupRoleRequirement requirement)
    {
        var userId = _userAccessor.Id;

        var user = await _dbContext.Users
            .Include(x => x.GroupUsers)
            .ThenInclude(x => x.Group)
            .Include(x => x.GroupUsers)
            .ThenInclude(x => x.GroupRole)
            .SingleOrDefaultAsync(x => x.Id == userId);
        var groupId = GetGroupIdFromRoute();

        if (groupId != null && user != null && IsUserInGroupWithRole(user, groupId.Value, requirement.RoleName))
        {
            context.Succeed(requirement);
        }
    }

    private static bool IsUserInGroupWithRole(ApplicationUser user, Guid groupId, string roleName)
    {
        var groupUser = user.GroupUsers.FirstOrDefault(gu => gu.UserId == user.Id && gu.GroupId == groupId && gu.GroupRole?.Name == roleName);
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

