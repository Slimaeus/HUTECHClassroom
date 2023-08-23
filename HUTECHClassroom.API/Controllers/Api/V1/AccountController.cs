using HUTECHClassroom.Application.Account.Commands.AddAvatar;
using HUTECHClassroom.Application.Account.Commands.ChangePassword;
using HUTECHClassroom.Application.Account.Commands.ForgotPassword;
using HUTECHClassroom.Application.Account.Commands.Login;
using HUTECHClassroom.Application.Account.Commands.Register;
using HUTECHClassroom.Application.Account.Commands.ResetPassword;
using HUTECHClassroom.Application.Account.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
[AllowAnonymous]
public class AccountController : BaseApiController
{
    [HttpPost("login")]
    public async Task<ActionResult<AccountDTO>> Login(LoginCommand request)
        => Ok(await Mediator.Send(request));
    // For test only
    [HttpPost("register")]
    public async Task<ActionResult<AccountDTO>> Register(RegisterCommand request)
        => Ok(await Mediator.Send(request));
    [Authorize]
    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand request)
    {
        await Mediator.Send(request);
        return NoContent();
    }
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand request)
    {
        var token = await Mediator.Send(request);
        return Ok(token);
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand request)
    {
        await Mediator.Send(request);
        return NoContent();
    }
    [Authorize]
    [HttpPost("add-avatar")]
    public async Task<IActionResult> AddAvatar(IFormFile file)
    {
        await Mediator.Send(new AddAvatarCommand(file));
        return NoContent();
    }
}
