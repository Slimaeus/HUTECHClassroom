using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomUsersWithPagination;

public class GetClassroomUsersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomUsersWithPaginationQuery, MemberDTO, PaginationParams>
{
    public GetClassroomUsersWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
