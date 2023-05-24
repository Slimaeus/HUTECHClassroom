using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Projects.DTOs;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupProjectsWithPagination;

public class GetGroupProjectsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetGroupProjectsWithPaginationQuery, ProjectDTO, PaginationParams>
{
    public GetGroupProjectsWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
