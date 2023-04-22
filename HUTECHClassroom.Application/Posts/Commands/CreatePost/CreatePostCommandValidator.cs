namespace HUTECHClassroom.Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Content).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Link).MaximumLength(200);

        RuleFor(x => x.UserName).NotEmpty().NotNull();
        RuleFor(x => x.ClassroomId).NotEmpty().NotNull();
    }
}
