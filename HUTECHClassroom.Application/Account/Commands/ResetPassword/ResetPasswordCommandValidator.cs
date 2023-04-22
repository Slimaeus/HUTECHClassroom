namespace HUTECHClassroom.Application.Account.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is not in the correct format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is requied.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }
}
