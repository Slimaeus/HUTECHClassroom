using HUTECHClassroom.Application.Account.Commands.ChangeEmail;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
[AllowAnonymous]
public sealed class AccountController : BaseApiController
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

    [Authorize]
    [HttpDelete("remove-avatar")]
    public async Task<IActionResult> RemoveAvatar()
    {
        await Mediator.Send(new RemoveAvatarCommand());
        return NoContent();
    }

    [Authorize]
    [HttpGet("@me")]
    public async Task<ActionResult<AccountDTO>> GetCurrentUserDetails()
        => Ok(await Mediator.Send(new GetCurrentUserQuery()));

    [Authorize]
    [HttpPost("change-email")]
    public async Task<IActionResult> ChangeEmail(ChangeEmailCommand request)
    {
        await Mediator.Send(request);
        return NoContent();
    }
}
