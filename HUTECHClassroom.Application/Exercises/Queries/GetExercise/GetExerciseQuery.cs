using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Exercises.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Exercises.Queries.GetExercise;

public record GetExerciseQuery(Guid Id) : GetQuery<ExerciseDTO>;
public class GetExerciseQueryHandler : GetQueryHandler<Exercise, GetExerciseQuery, ExerciseDTO>
{
    public GetExerciseQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    public override Expression<Func<Exercise, bool>> FilterPredicate(GetExerciseQuery query)
    {
        return x => x.Id == query.Id;
    }
    public override object GetNotFoundKey(GetExerciseQuery query)
    {
        return query.Id;
    }
}
