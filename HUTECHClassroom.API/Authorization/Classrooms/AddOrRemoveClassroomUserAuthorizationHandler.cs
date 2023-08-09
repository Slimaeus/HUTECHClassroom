using HUTECHClassroom.Domain.Constants.HttpParams;
using HUTECHClassroom.Domain.Entities;
using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.API.Authorization.Classrooms;

public class AddOrRemoveClassroomUserAuthorizationHandler : AuthorizationHandler<AddOrRemoveClassroomUserRequirement>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserAccessor _userAccessor;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddOrRemoveClassroomUserAuthorizationHandler(ApplicationDbContext dbContext, IUserAccessor userAccessor, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AddOrRemoveClassroomUserRequirement requirement)
    {
        var userId = _userAccessor.Id;

        var user = await _dbContext.Users
            .Include(x => x.Classrooms)
            .SingleOrDefaultAsync(x => x.Id == userId);
        var classroomId = GetClassroomIdFromRoute();

        if (classroomId != null && user != null && IsLecturerInClassroom(user, classroomId.Value))
        {
            context.Succeed(requirement);
        }
    }

    private static bool IsLecturerInClassroom(ApplicationUser user, Guid classroomId)
    {
        var isLecturer = user.Classrooms.Any(c => c.Id == classroomId);
        return isLecturer;
    }

    private Guid? GetClassroomIdFromRoute()
    {
        var routeData = _httpContextAccessor.HttpContext?.GetRouteData();
        if (routeData != null && routeData.Values.TryGetValue(ClassroomParamsConstants.CLASSROOM_ID, out var idValue))
        {
            if (Guid.TryParse(idValue.ToString(), out var classroomId))
            {
                return classroomId;
            }
        }
        return null;
    }
}
