using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Comments.Commands.UpdateComment;

public sealed class UpdateCommentCommandValidator : UpdateCommandValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(x => x.Content).MaximumLength(CommentConstants.CONTENT_MAX_LENGTH);
    }
}
