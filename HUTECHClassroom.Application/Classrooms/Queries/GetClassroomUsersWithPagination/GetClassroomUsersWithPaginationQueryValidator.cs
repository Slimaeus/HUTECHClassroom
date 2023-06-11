using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomUsersWithPagination;

public class GetClassroomUsersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomUsersWithPaginationQuery, ClassroomUserDTO, PaginationParams>
{
    public GetClassroomUsersWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
