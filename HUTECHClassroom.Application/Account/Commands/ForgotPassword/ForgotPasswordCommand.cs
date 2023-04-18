using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Application.Account.Commands.ForgotPassword;

public record ForgotPasswordCommand(string Email) : IRequest<string>;
public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, string>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        return token;
    }
}
