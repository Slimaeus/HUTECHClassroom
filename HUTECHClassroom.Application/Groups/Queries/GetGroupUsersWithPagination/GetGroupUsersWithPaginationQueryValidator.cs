using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupUsersWithPagination;

public sealed class GetGroupUsersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetGroupUsersWithPaginationQuery, GroupUserDTO, PaginationParams>
{
    public GetGroupUsersWithPaginationQueryValidator() : base()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
