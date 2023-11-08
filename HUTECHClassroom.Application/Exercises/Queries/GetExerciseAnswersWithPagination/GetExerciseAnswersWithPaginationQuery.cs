using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Answers.DTOs;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Exercises.Queries.GetExerciseAnswersWithPagination;

public sealed record GetExerciseAnswersWithPaginationQuery(Guid Id, ExercisePaginationParams Params) : GetWithPaginationQuery<AnswerDTO, ExercisePaginationParams>(Params);
public sealed class GetExerciseAnswersWithPaginationQueryHandler : GetWithPaginationQueryHandler<Answer, GetExerciseAnswersWithPaginationQuery, AnswerDTO, ExercisePaginationParams>
{
    public GetExerciseAnswersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Answer, bool>> FilterPredicate(GetExerciseAnswersWithPaginationQuery query)
        => x => x.ExerciseId == query.Id && (query.Params.UserId == null || query.Params.UserId == Guid.Empty || x.UserId == query.Params.UserId);
    protected override IQuery<Answer> Order(IMultipleResultQuery<Answer> query)
        => query.OrderByDescending(x => x.CreateDate);

    protected override Expression<Func<Answer, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => (x.Link != null && x.Link.ToLower().Contains(toLowerSearchString)) || x.Description.ToLower().Contains(toLowerSearchString);
    }
}
