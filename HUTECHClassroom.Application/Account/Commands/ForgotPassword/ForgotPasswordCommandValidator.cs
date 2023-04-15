using FluentValidation;

namespace HUTECHClassroom.Application.Account.Commands.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email is not in the correct format.");
        }
    }
}
