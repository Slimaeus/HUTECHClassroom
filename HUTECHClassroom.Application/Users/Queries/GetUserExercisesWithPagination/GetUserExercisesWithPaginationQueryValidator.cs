using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Exercises.DTOs;

namespace HUTECHClassroom.Application.Users.Queries.GetUserExercisesWithPagination;

public class GetUserExercisesWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetUserExercisesWithPaginationQuery, ExerciseDTO, PaginationParams>
{
}
