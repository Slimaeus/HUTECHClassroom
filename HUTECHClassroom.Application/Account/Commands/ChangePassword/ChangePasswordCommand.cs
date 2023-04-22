using HUTECHClassroom.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Application.Account.Commands.ChangePassword;

public record ChangePasswordCommand(string Password, string NewPassword) : IRequest<Unit>;
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserAccessor _userAccessor;

    public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager, IUserAccessor userAccessor)
    {
        _userManager = userManager;
        _userAccessor = userAccessor;
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(_userAccessor.UserName);
        if (user == null) throw new UnauthorizedAccessException(nameof(ApplicationUser));

        var result = await _userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);
        if (!result.Succeeded) throw new InvalidOperationException("Failed to change password");

        return Unit.Value;
    }
}
