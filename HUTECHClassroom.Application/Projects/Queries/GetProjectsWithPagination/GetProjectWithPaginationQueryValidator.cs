using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Projects.DTOs;

namespace HUTECHClassroom.Application.Projects.Queries.GetProjectsWithPagination;

public class GetProjectWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetProjectsWithPaginationQuery, ProjectDTO, PaginationParams> { }
