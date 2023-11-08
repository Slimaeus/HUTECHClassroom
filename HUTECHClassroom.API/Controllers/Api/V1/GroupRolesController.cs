namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public sealed class GroupRolesController : BaseEntityApiController<GroupRoleDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<GroupRoleDTO>>> Get([FromQuery] PaginationParams @params)
        => HandlePaginationQuery<GetGroupRolesWithPaginationQuery, PaginationParams>(new GetGroupRolesWithPaginationQuery(@params));
}
