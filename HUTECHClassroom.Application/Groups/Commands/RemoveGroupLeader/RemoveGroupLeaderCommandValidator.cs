namespace HUTECHClassroom.Application.Groups.Commands.RemoveGroupLeader;

public class RemoveGroupLeaderCommandValidator : AbstractValidator<RemoveGroupLeaderCommand>
{
    public RemoveGroupLeaderCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.UserId).NotNull().NotEmpty();
    }
}
