using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Exercises.DTOs;

namespace HUTECHClassroom.Application.Exercises.Queries.GetExercisesWithPagination;

public class GetExercisesWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetExercisesWithPaginationQuery, ExerciseDTO, PaginationParams>
{
    public GetExercisesWithPaginationQueryValidator()
    {
    }
}
