using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomUsersWithPagination;
using FluentValidation;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomUsersWithPagination
{
    public class GetClassroomUsersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomUsersWithPaginationQuery, MemberDTO>
    {
        public GetClassroomUsersWithPaginationQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
