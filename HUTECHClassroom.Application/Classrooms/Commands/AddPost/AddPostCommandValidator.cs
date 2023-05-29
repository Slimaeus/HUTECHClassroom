using HUTECHClassroom.Application.Common.Validators.Classrooms;
using HUTECHClassroom.Application.Common.Validators.Posts;

namespace HUTECHClassroom.Application.Classrooms.Commands.AddPost;

public class AddPostCommandValidator : AbstractValidator<AddPostCommand>
{
    public AddPostCommandValidator(ClassroomExistenceByIdValidator classroomIdValidator, PostExistenceByIdValidator postIdValidator)
    {
        RuleFor(x => x.Id).NotEmpty().NotNull()
            .SetValidator(classroomIdValidator);
        RuleFor(x => x.PostId).NotEmpty().NotNull()
            .SetValidator(postIdValidator);
    }
}
