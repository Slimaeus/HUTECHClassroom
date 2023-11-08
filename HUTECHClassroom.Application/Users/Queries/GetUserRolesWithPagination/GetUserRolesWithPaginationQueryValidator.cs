using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Roles.DTOs;

namespace HUTECHClassroom.Application.Users.Queries.GetUserRolesWithPagination;

public sealed class GetUserRolesWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetUserRolesWithPaginationQuery, RoleDTO, PaginationParams> { }
