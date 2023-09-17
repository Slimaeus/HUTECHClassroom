using HUTECHClassroom.Application.Common.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Application.Account.Commands.ResetPassword;

public record ResetPasswordCommand(string Email, string Code, string Password) : IRequest<Unit>;
public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager
            .FindByEmailAsync(request.Email)
            ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

        var result = await _userManager
            .ResetPasswordAsync(user, request.Code, request.Password);

        if (!result.Succeeded)
            throw new InvalidOperationException($"Failed to reset password: {string.Join(", ", result.Errors.Select(e => e.Description))}");

        return Unit.Value;
    }
}
