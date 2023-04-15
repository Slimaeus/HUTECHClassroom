using HUTECHClassroom.Application.Account.Commands.Login;
using HUTECHClassroom.Application.Account.Commands.Register;
using HUTECHClassroom.Application.Account.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginCommand request)
            => Ok(await Mediator.Send(request));
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterCommand request)
            => Ok(await Mediator.Send(request));
    }
}
