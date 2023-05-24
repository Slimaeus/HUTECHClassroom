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
    protected override Expression<Func<Mission, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
    protected override Expression<Func<Mission, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Title.ToLower().Contains(toLowerSearchString) || x.Description.ToLower().Contains(toLowerSearchString);
    }
}
