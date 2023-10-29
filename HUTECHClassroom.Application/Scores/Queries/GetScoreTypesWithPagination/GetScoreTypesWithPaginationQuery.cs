using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Scores.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Scores.Queries.GetScoreTypesWithPagination;

public sealed record GetScoreTypesWithPaginationQuery(PaginationParams Params)
    : GetWithPaginationQuery<ScoreTypeDTO, PaginationParams>(Params);

public sealed class GetScoreTypesWithPaginationQueryHandler
    : GetWithPaginationQueryHandler<int, ScoreType, GetScoreTypesWithPaginationQuery, ScoreTypeDTO, PaginationParams>
{
    public GetScoreTypesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<ScoreType, bool>> SearchStringPredicate(string searchString)
        => x => x.Name.ToLower().Contains(searchString.ToLower());
    protected override IQuery<ScoreType> Order(IMultipleResultQuery<ScoreType> query)
        => query.OrderBy(x => x.Id);
}
