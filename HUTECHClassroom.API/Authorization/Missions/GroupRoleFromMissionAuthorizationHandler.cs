using HUTECHClassroom.API.Authorization.GroupRoles;
using HUTECHClassroom.Domain.Constants.HttpParams;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Persistence;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace HUTECHClassroom.API.Authorization.Missions;

public class GroupRoleFromMissionAuthorizationHandler : GroupRoleAuthorizationHandler<GroupRoleFromMissionRequirement>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GroupRoleFromMissionAuthorizationHandler(ApplicationDbContext dbContext, IUserAccessor userAccessor, IHttpContextAccessor httpContextAccessor) : base(dbContext, userAccessor, httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<Guid?> GetGroupIdAsync()
    {
        var groupIdByProjectIdFromResponseBody = await GetGroupIdByProjectIdFromResponseBodyAsync();

        if (groupIdByProjectIdFromResponseBody != null) return groupIdByProjectIdFromResponseBody;

        var groupIdByMissionId = await GetGroupIdByMissionIdAsync();

        if (groupIdByMissionId != null) return groupIdByMissionId;

        return null;
    }

    private async Task<Guid?> GetGroupIdByProjectIdFromResponseBodyAsync()
    {
        using var requestReader = new StreamReader(_httpContextAccessor.HttpContext?.Request.Body);
        var body = await requestReader.ReadToEndAsync();
        var projectIdBody = JsonConvert.DeserializeObject<ProjectIdBody>(body);
        _httpContextAccessor.HttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
        if (projectIdBody == null || projectIdBody.ProjectId == Guid.Empty)
        {
            return null;
        }
        var projectId = projectIdBody.ProjectId;

        return await GetGroupIdFromDbContextAsync(projectId);
    }

    private async Task<Guid?> GetGroupIdByMissionIdAsync()
    {
        var routeData = _httpContextAccessor.HttpContext?.GetRouteData();

        if (routeData != null && routeData.Values.TryGetValue(MissionParamsConstants.MISSION_ID, out var idValue))
        {
            var doesParseMissionIdSuccess = Guid.TryParse(idValue.ToString(), out var missionId);

            if (!doesParseMissionIdSuccess) return null;

            var mission = await _dbContext.Missions
                .Include(m => m.Project)
                .SingleOrDefaultAsync(m => m.Id == missionId);

            if (mission == null) return null;

            return mission.Project.GroupId;
        };

        return null;
    }

}
