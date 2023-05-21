namespace HUTECHClassroom.Application.Groups.Commands.AddGroupLeader;
internal class AddGroupLeaderCommandValidator : AbstractValidator<AddGroupLeaderCommand>
{
    public AddGroupLeaderCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.UserName).NotNull().NotEmpty();
    }
}
