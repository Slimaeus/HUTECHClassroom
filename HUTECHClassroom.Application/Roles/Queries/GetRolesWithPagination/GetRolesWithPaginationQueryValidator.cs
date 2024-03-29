﻿using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Roles.DTOs;

namespace HUTECHClassroom.Application.Roles.Queries.GetRolesWithPagination;

public sealed class GetRolesWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetRolesWithPaginationQuery, RoleDTO, RolePaginationParams> { }
