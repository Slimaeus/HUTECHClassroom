using HUTECHClassroom.API.Authorization.GroupRoles;
using HUTECHClassroom.Domain.Constants.HttpParams;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Persistence;
using Newtonsoft.Json;
using System.Text;

namespace HUTECHClassroom.API.Authorization.Projects;

public sealed class GroupRoleFromProjectAuthorizationHandler : GroupRoleAuthorizationHandler<GroupRoleFromProjectRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GroupRoleFromProjectAuthorizationHandler(ApplicationDbContext dbContext, IUserAccessor userAccessor, IHttpContextAccessor httpContextAccessor) : base(dbContext, userAccessor, httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<Guid?> GetGroupIdAsync()
    {
        var groupIdFromResponseBody = await GetGroupIdFromResponseBodyAsync();

        if (groupIdFromResponseBody != null) return groupIdFromResponseBody;

        var groupIdFromProjectId = await GetGroupIdByProjectIdAsync();

        if (groupIdFromProjectId != null) return groupIdFromProjectId;

        return null;
    }

    private async Task<Guid?> GetGroupIdFromResponseBodyAsync()
    {
        if (_httpContextAccessor.HttpContext?.Request is null && _httpContextAccessor.HttpContext?.Request.Body is null) return null;
        using var requestReader = new StreamReader(_httpContextAccessor.HttpContext.Request.Body);
        var body = await requestReader.ReadToEndAsync();
        var groupIdBody = JsonConvert.DeserializeObject<GroupIdBody>(body);
        _httpContextAccessor.HttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
        if (groupIdBody == null || groupIdBody.GroupId == Guid.Empty)
        {
            return null;
        }
        return groupIdBody.GroupId;
    }

    private async Task<Guid?> GetGroupIdByProjectIdAsync()
    {
        var routeData = _httpContextAccessor.HttpContext?.GetRouteData();

        if (routeData is { } && routeData.Values.TryGetValue(ProjectParamsConstants.PROJECT_ID, out var idValue))
        {
            if (idValue is null) return null;
            var doesParseProjectIdSuccess = Guid.TryParse(idValue.ToString(), out var projectId);

            if (!doesParseProjectIdSuccess) return null;

            return await GetGroupIdFromDbContextAsync(projectId);
        };

        return null;
    }
}
