using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Groups.Queries.GetGroupUsersWithPagination;

namespace HUTECHClassroom.Application.Exercises.Queries.GetExerciseUsersWithPagination;

public class GetGroupnUsersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetGroupUsersWithPaginationQuery, MemberDTO>
{
    public GetGroupnUsersWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
