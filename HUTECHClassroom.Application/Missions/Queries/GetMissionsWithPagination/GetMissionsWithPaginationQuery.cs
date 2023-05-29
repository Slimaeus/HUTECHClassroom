using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionsWithPagination;

public record GetMissionsWithPaginationQuery(MissionPaginationParams Params) : GetWithPaginationQuery<MissionDTO, MissionPaginationParams>(Params);
public class GetMissionsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Mission, GetMissionsWithPaginationQuery, MissionDTO, MissionPaginationParams>
{

    public GetMissionsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Mission, bool>> SearchStringPredicate(string searchString)
        => x => x.Title.ToLower().Contains(searchString.ToLower()) || x.Description.ToLower().Contains(searchString.ToLower());
    protected override IQuery<Mission> Order(IMultipleResultQuery<Mission> query) => query.OrderBy(x => x.CreateDate);

    protected override IMultipleResultQuery<Mission> SortingQuery(IMultipleResultQuery<Mission> query, GetMissionsWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.TitleOrder, x => x.Title)
                .SortEntityQuery(request.Params.DescriptionOrder, x => x.Description)
                .SortEntityQuery(request.Params.IsDoneOrder, x => x.IsDone);
}

