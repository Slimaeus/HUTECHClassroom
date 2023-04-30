using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupProjectsWithPagination;

public class GetGroupProjectsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetGroupProjectsWithPaginationQuery, GroupProjectDTO, PaginationParams>
{
    public GetGroupProjectsWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
