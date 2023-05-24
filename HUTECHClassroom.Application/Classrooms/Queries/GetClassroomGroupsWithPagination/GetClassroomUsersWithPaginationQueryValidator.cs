using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomGroupsWithPagination;

public class GetClassroomGroupsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomGroupsWithPaginationQuery, GroupDTO, PaginationParams>
{
    public GetClassroomGroupsWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
