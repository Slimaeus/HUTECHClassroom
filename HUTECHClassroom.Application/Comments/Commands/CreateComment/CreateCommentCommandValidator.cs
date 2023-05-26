using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Comments.Commands.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.Content).NotEmpty().MaximumLength(CommentConstants.CONTENT_MAX_LENGTH);

        RuleFor(x => x.UserName).NotEmpty().NotNull();
        RuleFor(x => x.PostId).NotEmpty().NotNull();
    }
}
