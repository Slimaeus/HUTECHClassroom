using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionsWithPagination;

public record GetMissionsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<MissionDTO>(Params);
public class GetMissionsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Mission, GetMissionsWithPaginationQuery, MissionDTO>
{
    public GetMissionsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

    protected override Expression<Func<Mission, bool>> SearchStringPredicate(string searchString)
        => x => x.Title.ToLower().Contains(searchString.ToLower()) || x.Description.ToLower().Contains(searchString.ToLower());
    protected override Expression<Func<Mission, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
}

