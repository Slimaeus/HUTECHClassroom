
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Application.Account.Commands.ChangeEmail;

public sealed record ChangeEmailCommand(string NewEmail) : IRequest<Unit>;
public sealed class Handler : IRequestHandler<ChangeEmailCommand, Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserAccessor _userAccessor;

    public Handler(UserManager<ApplicationUser> userManager, IUserAccessor userAccessor)
    {
        _userManager = userManager;
        _userAccessor = userAccessor;
    }
    public async Task<Unit> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(_userAccessor.UserName)
            ?? throw new UnauthorizedAccessException(nameof(ApplicationUser));
        var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);

        var result = await _userManager.ChangeEmailAsync(user, request.NewEmail, token);

        if (!result.Succeeded) throw new InvalidOperationException("Failed to change email");

        return Unit.Value;
    }
}

public sealed class Validator : AbstractValidator<ChangeEmailCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public Validator(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        RuleFor(x => x.NewEmail)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not in the correct format.")
            .MustAsync(IsUniqueEmail).WithMessage("The specified Email is already in use.");
    }

    private async Task<bool> IsUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _userManager.FindByEmailAsync(email) == null && await _userManager.FindByNameAsync(email) == null; ;
    }
}
