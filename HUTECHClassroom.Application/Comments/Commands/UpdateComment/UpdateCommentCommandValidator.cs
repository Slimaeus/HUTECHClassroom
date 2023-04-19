using FluentValidation;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Comments.Commands.UpdateComment;

public class UpdateCommentCommandValidator : UpdateCommandValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(x => x.Content).NotEmpty().MaximumLength(500);
    }
}
