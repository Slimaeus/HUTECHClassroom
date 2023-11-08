namespace HUTECHClassroom.API.Controllers.Api.V1;

[Authorize]
[ApiVersion("1.0")]
public sealed class RolesController : BaseEntityApiController<RoleDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<RoleDTO>>> Get([FromQuery] PaginationParams @params)
        => HandlePaginationQuery<GetUserRolesWithPaginationQuery, PaginationParams>(new GetUserRolesWithPaginationQuery(@params));
}
