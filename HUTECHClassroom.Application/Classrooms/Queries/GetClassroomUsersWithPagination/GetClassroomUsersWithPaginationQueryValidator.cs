using FluentValidation;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomUsersWithPagination;

public class GetClassroomUsersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomUsersWithPaginationQuery, MemberDTO>
{
    public GetClassroomUsersWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
