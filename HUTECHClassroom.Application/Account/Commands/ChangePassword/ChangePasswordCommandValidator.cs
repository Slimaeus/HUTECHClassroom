namespace HUTECHClassroom.Application.Account.Commands.ChangePassword;

public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        //RuleFor(x => x.UserName)
        //    .NotEmpty().WithMessage("UserName is required.")
        //    .MinimumLength(3).WithMessage("UserName must be at least 3 characters long.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is requied.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New Password is requied.")
            .NotEqual(x => x.Password).WithMessage("New Password must be different from Password.")
            .MinimumLength(8).WithMessage("New Password must be at least 8 characters long.");
    }
}
