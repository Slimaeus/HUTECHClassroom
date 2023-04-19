using FluentValidation;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommandValidator : UpdateCommandValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(x => x.Content).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Link).MaximumLength(200);
    }
}
