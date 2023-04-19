using FluentValidation;

namespace HUTECHClassroom.Application.Classrooms.Commands.RemovePost;

public class RemovePostCommandValidator : AbstractValidator<RemovePostCommand>
{
    public RemovePostCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.PostId).NotEmpty().NotNull();
    }
}
