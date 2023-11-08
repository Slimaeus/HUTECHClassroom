using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Posts.Commands.UpdatePost;

public sealed class UpdatePostCommandValidator : UpdateCommandValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(x => x.Content).NotEmpty().MaximumLength(PostConstants.CONTENT_MAX_LENGTH);
        RuleFor(x => x.Link).MaximumLength(CommonConstants.LINK_MAX_LENGTH);
    }
}
