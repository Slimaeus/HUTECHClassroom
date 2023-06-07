using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

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
            .Include(x => x.Groups)
            .Include(x => x.GroupUsers)
            .ThenInclude(x => x.Group)
            .Include(x => x.GroupUsers)
            .ThenInclude(x => x.GroupRole)
            .AsSplitQuery()
            .SingleOrDefaultAsync(x => x.Id == userId);
        var groupId = await GetGroupIdFromRouteAsync();

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

    private async Task<Guid?> GetGroupIdFromRouteAsync()
    {
        var routeData = _httpContextAccessor.HttpContext?.GetRouteData();
        if (routeData != null && routeData.Values.TryGetValue("groupId", out var idValue))
        {
            if (Guid.TryParse(idValue.ToString(), out var groupId))
            {
                return groupId;
            }
        }

        using var requestReader = new StreamReader(_httpContextAccessor.HttpContext?.Request.Body);
        var body = await requestReader.ReadToEndAsync();
        var groupIdBody = JsonConvert.DeserializeObject<GroupIdBody>(body);
        _httpContextAccessor.HttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
        if (groupIdBody.GroupId != Guid.Empty)
        {
            return groupIdBody.GroupId;
        }
        return null;
    }

    record GroupIdBody(Guid GroupId);
}

