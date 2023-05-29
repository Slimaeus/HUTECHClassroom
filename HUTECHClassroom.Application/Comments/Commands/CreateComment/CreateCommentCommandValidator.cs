using HUTECHClassroom.Application.Common.Validators.Posts;
using HUTECHClassroom.Application.Common.Validators.Users;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Comments.Commands.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator(UserExistenceByNotNullIdValidator userIdValidator, PostExistenceByNotNullIdValidator postIdValidator)
    {
        RuleFor(x => x.Content).NotEmpty().MaximumLength(CommentConstants.CONTENT_MAX_LENGTH);

        RuleFor(x => x.UserId).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
        RuleFor(x => x.PostId).NotEmpty().NotNull()
            .SetValidator(postIdValidator);
    }
}
