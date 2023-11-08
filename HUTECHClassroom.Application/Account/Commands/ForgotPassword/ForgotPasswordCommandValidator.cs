namespace HUTECHClassroom.Application.Account.Commands.ForgotPassword;

public sealed class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
        => RuleFor(x => x.Email)
            .NotNull().WithMessage("Email is required")
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not in the correct format.");
}
