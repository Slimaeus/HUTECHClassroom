﻿using HUTECHClassroom.Application.Account.Commands.ChangePassword;
using HUTECHClassroom.Application.Account.Commands.ForgotPassword;
using HUTECHClassroom.Application.Account.Commands.Login;
using HUTECHClassroom.Application.Account.Commands.Register;
using HUTECHClassroom.Application.Account.Commands.ResetPassword;
using HUTECHClassroom.Application.Account.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class AccountController : BaseApiController
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginCommand request)
        => Ok(await Mediator.Send(request));
    // For test only
    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterCommand request)
        => Ok(await Mediator.Send(request));
    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand request)
    {
        await Mediator.Send(request);
        return NoContent();
    }
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand request)
    {
        //await Mediator.Send(request);
        //return NoContent(); --> Use this when finish

        var token = await Mediator.Send(request);
        return Ok(token);
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand request)
    {
        await Mediator.Send(request);
        return NoContent();
    }
}
