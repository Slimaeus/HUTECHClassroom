using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Scores.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.ScoreTypes.Queries.GetScoreType;

public sealed record GetScoreTypeQuery(int Id) : GetQuery<ScoreTypeDTO>;
public sealed class GetScoreTypeQueryHandler : GetQueryHandler<int, ScoreType, GetScoreTypeQuery, ScoreTypeDTO>
{
    public GetScoreTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override Expression<Func<ScoreType, bool>> FilterPredicate(GetScoreTypeQuery query)
        => x => x.Id == query.Id;
    public override object GetNotFoundKey(GetScoreTypeQuery query)
        => query.Id;
}
