using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Exercises.DTOs;

namespace HUTECHClassroom.Application.Exercises.Queries.GetExerciseUsersWithPagination;

public sealed class GetExerciseUsersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetExerciseUsersWithPaginationQuery, ExerciseUserDTO, PaginationParams>
{
    public GetExerciseUsersWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
