namespace HUTECHClassroom.Application.Groups.Commands.AddGroupUser;

public class AddGroupUserCommandValidator : AbstractValidator<AddGroupUserCommand>
{
    public AddGroupUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.UserId).NotEmpty().NotNull();
    }
}
