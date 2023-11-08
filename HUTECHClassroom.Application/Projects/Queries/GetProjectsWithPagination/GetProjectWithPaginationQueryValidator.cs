using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Projects.DTOs;

namespace HUTECHClassroom.Application.Projects.Queries.GetProjectsWithPagination;

public sealed class GetProjectWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetProjectsWithPaginationQuery, ProjectDTO, ProjectPaginationParams> { }
