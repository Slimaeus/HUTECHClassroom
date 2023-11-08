using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Projects.DTOs;

namespace HUTECHClassroom.Application.Users.Queries.GetUserProjectsWithPagination;

public sealed class GetUserProjectsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetUserProjectsWithPaginationQuery, ProjectDTO, PaginationParams>
{
}
