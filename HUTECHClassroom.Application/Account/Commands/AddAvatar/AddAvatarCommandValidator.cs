namespace HUTECHClassroom.Application.Account.Commands.AddAvatar;

public class AddAvatarCommandValidator : AbstractValidator<AddAvatarCommand>
{
    public AddAvatarCommandValidator()
    {
        RuleFor(x => x.File)
            .NotEmpty().WithMessage("Please provide image!")
            .NotNull().WithMessage("Please provide image!");
    }
}
