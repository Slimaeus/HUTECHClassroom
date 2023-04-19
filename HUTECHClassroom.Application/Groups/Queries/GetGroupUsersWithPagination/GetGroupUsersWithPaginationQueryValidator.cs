using FluentValidation;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupUsersWithPagination;

public class GetGroupUsersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetGroupUsersWithPaginationQuery, MemberDTO>
{
    public GetGroupUsersWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
