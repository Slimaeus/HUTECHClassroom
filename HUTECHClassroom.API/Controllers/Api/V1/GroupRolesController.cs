﻿using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.GroupRoles.DTOs;
using HUTECHClassroom.Application.GroupRoles.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class GroupRolesController : BaseEntityApiController<GroupRoleDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<GroupRoleDTO>>> Get([FromQuery] PaginationParams @params)
        => HandlePaginationQuery<GetGroupRolesWithPaginationQuery, PaginationParams>(new GetGroupRolesWithPaginationQuery(@params));
}
