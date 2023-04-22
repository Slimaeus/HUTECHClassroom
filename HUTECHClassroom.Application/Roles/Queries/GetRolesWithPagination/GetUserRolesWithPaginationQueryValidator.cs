using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Roles.DTOs;
using HUTECHClassroom.Application.Users.Queries.GetUserRolesWithPagination;

namespace HUTECHClassroom.Application.Roles.Queries.GetRolesWithPagination;

public class GetUserRolesWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetUserRolesWithPaginationQuery, RoleDTO> { }
