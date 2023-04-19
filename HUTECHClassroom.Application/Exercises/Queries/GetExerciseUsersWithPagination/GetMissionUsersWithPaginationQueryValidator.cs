using FluentValidation;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupUsersWithPagination;

public class GetGroupnUsersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetGroupUsersWithPaginationQuery, MemberDTO>
{
    public GetGroupnUsersWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
