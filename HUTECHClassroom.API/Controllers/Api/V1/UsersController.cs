using HUTECHClassroom.Application.Users.DTOs;
using HUTECHClassroom.Application.Users.Queries.GetUser;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class UsersController : BaseEntityApiController<UserDTO>
{
    [HttpGet("{userName}")]
    public Task<ActionResult<UserDTO>> GetUserDetails(string userName)
        => HandleGetQuery(new GetUserQuery(userName));
}
