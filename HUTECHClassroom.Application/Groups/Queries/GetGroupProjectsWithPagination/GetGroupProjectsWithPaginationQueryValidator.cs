using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Projects.DTOs;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupProjectsWithPagination;

public sealed class GetGroupProjectsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetGroupProjectsWithPaginationQuery, ProjectDTO, GroupPaginationParams>
{
    public GetGroupProjectsWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
