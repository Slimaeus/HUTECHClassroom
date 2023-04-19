using FluentValidation;

namespace HUTECHClassroom.Application.Classrooms.Commands.AddPost;

public class AddPostCommandValidator : AbstractValidator<AddPostCommand>
{
    public AddPostCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
        RuleFor(x => x.PostId).NotEmpty().NotNull();
    }
}
