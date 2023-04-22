using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Exercises.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Exercises.Queries.GetExercisesWithPagination;

public record GetExercisesWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<ExerciseDTO>(Params);
public class GetExercisesWithPaginationQueryHandler : GetWithPaginationQueryHandler<Exercise, GetExercisesWithPaginationQuery, ExerciseDTO>
{
    public GetExercisesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Exercise, bool>> SearchStringPredicate(string searchString)
    {
        return x => x.Title.ToLower().Equals(searchString.ToLower())
        || x.Instruction.ToLower().Equals(searchString.ToLower())
        || x.Link.ToLower().Equals(searchString.ToLower())
        || x.Topic.ToLower().Equals(searchString.ToLower());
    }
    protected override Expression<Func<Exercise, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
}
