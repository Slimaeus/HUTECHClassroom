using HUTECHClassroom.Application.Common.Validators.Classrooms;
using HUTECHClassroom.Application.Common.Validators.Posts;

namespace HUTECHClassroom.Application.Classrooms.Commands.AddPost;

public sealed class AddPostCommandValidator : AbstractValidator<AddPostCommand>
{
    public AddPostCommandValidator(ClassroomExistenceByNotNullIdValidator classroomIdValidator, PostExistenceByNotNullIdValidator postIdValidator)
    {
        RuleFor(x => x.ClassroomId).NotEmpty().NotNull()
            .SetValidator(classroomIdValidator);
        RuleFor(x => x.PostId).NotEmpty().NotNull()
            .SetValidator(postIdValidator);
    }
}
