using HUTECHClassroom.Application.Common.Validators.Classrooms;
using HUTECHClassroom.Application.Common.Validators.Users;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator(UserExistenceByNotNullIdValidator userIdValidator, ClassroomExistenceByNotNullIdValidator classroomIdValidator)
    {
        RuleFor(x => x.Content).NotNull().NotEmpty().MaximumLength(PostConstants.CONTENT_MAX_LENGTH);
        RuleFor(x => x.Link).MaximumLength(CommonConstants.LINK_MAX_LENGTH);

        RuleFor(x => x.UserId).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
        RuleFor(x => x.ClassroomId).NotEmpty().NotNull()
            .SetValidator(classroomIdValidator);
    }
}
