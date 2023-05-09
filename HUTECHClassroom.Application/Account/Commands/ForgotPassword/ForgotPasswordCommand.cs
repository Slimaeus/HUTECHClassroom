using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Application.Account.Commands.ForgotPassword;

public record ForgotPasswordCommand(string Email) : IRequest<string>;
public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }
    public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        await _emailService.SendEmailAsync(user.Email, "Reset Password Code", $"This is reset password code: {token}");

        return token;
    }
}
