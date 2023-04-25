using HUTECHClassroom.Application.Users.DTOs;
using HUTECHClassroom.Application.Users.Queries.GetUser;
using HUTECHClassroom.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class UsersController : BaseEntityApiController<UserDTO>
{
    private readonly IUserAccessor _userAccessor;

    public UsersController(IUserAccessor userAccessor)
    {
        _userAccessor = userAccessor;
    }
    [Authorize]
    [HttpGet("get-roles")]
    public ActionResult<IList<string>> GetRoles()
    {
        return Ok(_userAccessor.Roles);
    }
    [Authorize]
    [HttpGet("get-entity-claims")]
    public ActionResult<IDictionary<string, ImmutableArray<string>>> GetEntityClaims()
    {
        return Ok(_userAccessor.EntityClaims);
    }
    [HttpGet("{userName}")]
    public Task<ActionResult<UserDTO>> GetUserDetails(string userName)
        => HandleGetQuery(new GetUserQuery(userName));
}
