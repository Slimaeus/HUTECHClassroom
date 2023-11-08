using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Constants.HttpParams;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.API.Authorization.GroupRoles;

public class GroupRoleAuthorizationHandler<TRequiremt> : AuthorizationHandler<TRequiremt>
    where TRequiremt : GroupRoleRequirement
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

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequiremt requirement)
    {
        var userId = _userAccessor.Id;

        var user = await _dbContext.Users
            .Include(x => x.Groups)
            .Include(x => x.GroupUsers)
            .ThenInclude(x => x.Group)
            .Include(x => x.GroupUsers)
            .ThenInclude(x => x.GroupRole)
            .AsSplitQuery()
            .SingleOrDefaultAsync(x => x.Id == userId);
        var groupId = await GetGroupIdAsync();

        if (groupId != null && user != null)
        {
            switch (requirement.RoleName)
            {
                case GroupRoleConstants.LEADER:
                    if (IsLeader(user, groupId.Value))
                        context.Succeed(requirement);
                    break;
                case GroupRoleConstants.MEMBER:
                    if (IsUserInGroupWithRole(user, groupId.Value, GroupRoleConstants.MEMBER))
                        context.Succeed(requirement);
                    break;
                default:
                    break;
            }
        }
    }
    private static bool IsLeader(ApplicationUser user, Guid groupId)
    {
        return user.Groups.Any(g => g.Id == groupId);
    }
    private static bool IsUserInGroupWithRole(ApplicationUser user, Guid groupId, string roleName)
    {
        var groupUser = user.GroupUsers.FirstOrDefault(gu => gu.GroupId == groupId && gu.GroupRole?.Name == roleName);
        return groupUser != null;
    }

    protected virtual Task<Guid?> GetGroupIdAsync()
    {
        var groupIdFromRoute = GetGroupIdFromRoute();

        if (groupIdFromRoute != null) return Task.FromResult(groupIdFromRoute);

        return Task.FromResult<Guid?>(null);
    }

    private Guid? GetGroupIdFromRoute()
    {
        var routeData = _httpContextAccessor.HttpContext?.GetRouteData();
        if (routeData != null && routeData.Values.TryGetValue(GroupParamsConstants.GROUP_ID, out var idValue))
        {
            if (Guid.TryParse(idValue.ToString(), out var groupId))
            {
                return groupId;
            }
        }
        return null;
    }

    protected async Task<Guid?> GetGroupIdFromDbContextAsync(Guid projectId)
    {
        var project = await _dbContext.Projects.FindAsync(projectId);

        if (project == null) return null;

        return project.GroupId;
    }

    protected record GroupIdBody(Guid GroupId);
    protected record ProjectIdBody(Guid ProjectId);
}

public sealed class GroupRoleAuthorizationHandler : GroupRoleAuthorizationHandler<GroupRoleRequirement>
{
    public GroupRoleAuthorizationHandler(ApplicationDbContext dbContext, IUserAccessor userAccessor, IHttpContextAccessor httpContextAccessor) : base(dbContext, userAccessor, httpContextAccessor)
    {
    }
}

