using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Application.Groups.Queries.GetGroupUsersWithPagination;

namespace HUTECHClassroom.Application.Exercises.Queries.GetExerciseUsersWithPagination;

public class GetGroupnUsersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetGroupUsersWithPaginationQuery, GroupUserDTO, PaginationParams>
{
    public GetGroupnUsersWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
