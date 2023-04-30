using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Roles.DTOs;
using HUTECHClassroom.Application.Users.Queries.GetUserRolesWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[Authorize]
[ApiVersion("1.0")]
public class RolesController : BaseEntityApiController<RoleDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<RoleDTO>>> Get([FromQuery] PaginationParams @params)
        => HandlePaginationQuery<GetUserRolesWithPaginationQuery, PaginationParams>(new GetUserRolesWithPaginationQuery(@params));
}
