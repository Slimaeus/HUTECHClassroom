using HUTECHClassroom.Application.Account.DTOs;
using HUTECHClassroom.Application.Account.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginQuery request)
            => Ok(await Mediator.Send(request));
    }
}
