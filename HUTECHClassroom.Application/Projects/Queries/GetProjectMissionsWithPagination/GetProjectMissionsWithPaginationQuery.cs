using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Projects.Queries.GetProjectMissionsWithPagination;

public record GetProjectMissionsWithPaginationQuery(Guid Id, ProjectPaginationParams Params) : GetWithPaginationQuery<MissionDTO, ProjectPaginationParams>(Params);
public class GetProjectMissionsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Mission, GetProjectMissionsWithPaginationQuery, MissionDTO, ProjectPaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetProjectMissionsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
    }
    protected override Expression<Func<Mission, bool>> FilterPredicate(GetProjectMissionsWithPaginationQuery query)
    {
        return x => x.ProjectId == query.Id && (query.Params.UserId == null || query.Params.UserId == Guid.Empty || x.MissionUsers.Any(mu => query.Params.UserId == mu.UserId));
    }
    protected override IMappingParams GetMappingParameters()
    {
        return new UserMappingParams { UserId = _userAccessor.Id };
    }
    protected override IQuery<Mission> Order(IMultipleResultQuery<Mission> query) => query.OrderByDescending(x => x.CreateDate);

    protected override Expression<Func<Mission, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Title.ToLower().Contains(toLowerSearchString) || x.Description.ToLower().Contains(toLowerSearchString);
    }
}
