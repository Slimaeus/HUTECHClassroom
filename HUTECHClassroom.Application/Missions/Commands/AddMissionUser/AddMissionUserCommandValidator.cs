using FluentValidation;

namespace HUTECHClassroom.Application.Missions.Commands.AddMissionUser;

public class AddMissionUserCommandValidator : AbstractValidator<AddMissionUserCommand>
{
    public AddMissionUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.UserName).NotEmpty().NotNull();
    }
}
