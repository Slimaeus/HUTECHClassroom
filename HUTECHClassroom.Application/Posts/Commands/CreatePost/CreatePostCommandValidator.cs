using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Content).NotNull().NotEmpty().MaximumLength(PostConstants.CONTENT_MAX_LENGTH);
        RuleFor(x => x.Link).MaximumLength(CommonConstants.LINK_MAX_LENGTH);

        RuleFor(x => x.UserName).NotEmpty().NotNull();
        RuleFor(x => x.ClassroomId).NotEmpty().NotNull();
    }
}
