namespace HUTECHClassroom.Application.Classrooms.Commands.RemovePost;

public sealed class RemovePostCommandValidator : AbstractValidator<RemovePostCommand>
{
    public RemovePostCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.PostId).NotEmpty().NotNull();
    }
}
