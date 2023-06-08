using HUTECHClassroom.Application.Common.Validators.Exercises;
using HUTECHClassroom.Application.Common.Validators.Users;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Answers.Commands.CreateAnswer;

public class CreateAnswerCommandValidator : AbstractValidator<CreateAnswerCommand>
{
    public CreateAnswerCommandValidator(UserExistenceByNotNullIdValidator userIdValidator, ExerciseExistenceByNotNullIdValidator exerciseIdVaidator)
    {
        RuleFor(x => x.Description)
            .MaximumLength(AnswerConstants.DESCRIPTION_MAX_LENGTH)
            .NotEmpty().NotNull();

        RuleFor(x => x.Link)
            .MaximumLength(CommonConstants.LINK_MAX_LENGTH)

        RuleFor(x => x.UserId).NotEmpty().NotNull()
            .SetValidator(userIdValidator);
        RuleFor(x => x.ExerciseId).NotEmpty().NotNull()
            .SetValidator(exerciseIdVaidator);

    }
}
