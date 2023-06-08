using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Exercises.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Exercises.Queries.GetExercisesWithPagination;

public record GetExercisesWithPaginationQuery(ExercisePaginationParams Params) : GetWithPaginationQuery<ExerciseDTO, ExercisePaginationParams>(Params);
public class GetExercisesWithPaginationQueryHandler : GetWithPaginationQueryHandler<Exercise, GetExercisesWithPaginationQuery, ExerciseDTO, ExercisePaginationParams>
{
    public GetExercisesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Exercise, bool>> FilterPredicate(GetExercisesWithPaginationQuery query)
    {
        return x => query.Params.UserId == null || query.Params.UserId == Guid.Empty || x.ExerciseUsers.Any(eu => eu.UserId == query.Params.UserId);
    }
    protected override Expression<Func<Exercise, bool>> SearchStringPredicate(string searchString)
        => x => x.Title.ToLower().Equals(searchString.ToLower())
                || x.Instruction.ToLower().Equals(searchString.ToLower())
                || x.Link.ToLower().Equals(searchString.ToLower())
                || x.Topic.ToLower().Equals(searchString.ToLower());
    protected override IQuery<Exercise> Order(IMultipleResultQuery<Exercise> query) => query.OrderByDescending(x => x.CreateDate);
    protected override IMultipleResultQuery<Exercise> SortingQuery(IMultipleResultQuery<Exercise> query, GetExercisesWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.TitleOrder, x => x.Title)
                .SortEntityQuery(request.Params.InstructionOrder, x => x.Instruction)
                .SortEntityQuery(request.Params.LinkOrder, x => x.Link)
                .SortEntityQuery(request.Params.TotalScoreOrder, x => x.TotalScore)
                .SortEntityQuery(request.Params.DeadlineOrder, x => x.Deadline)
                .SortEntityQuery(request.Params.TopicOrder, x => x.Topic)
                .SortEntityQuery(request.Params.CriteriaOrder, x => x.Criteria);
}
