using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Projects.Queries.GetProjectMissionsWithPagination;

public record GetProjectMissionsWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<MissionDTO, PaginationParams>(Params);
public class GetProjectMissionsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Mission, GetProjectMissionsWithPaginationQuery, MissionDTO, PaginationParams>
{
    public GetProjectMissionsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Mission, bool>> FilterPredicate(GetProjectMissionsWithPaginationQuery query)
    {
        return x => x.ProjectId == query.Id;
    }
    protected override IQuery<Mission> Order(IMultipleResultQuery<Mission> query) => query.OrderBy(x => x.CreateDate);

    protected override Expression<Func<Mission, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Title.ToLower().Contains(toLowerSearchString) || x.Description.ToLower().Contains(toLowerSearchString);
    }
}
