using FluentValidation;

namespace HUTECHClassroom.Application.Missions.Commands.RemoveMissionUser;

public class RemoveMissionUserCommandValidator : AbstractValidator<RemoveMissionUserCommand>
{
    public RemoveMissionUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.UserName).NotEmpty().NotNull();
    }
}
