using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Answers.Commands.CreateAnswer;

public class CreateAnswerCommandValidator : AbstractValidator<CreateAnswerCommand>
{
    public CreateAnswerCommandValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(AnswerConstants.DESCRIPTION_MAX_LENGTH)
            .NotEmpty().NotNull();

        RuleFor(x => x.Link)
            .MaximumLength(CommonConstants.LINK_MAX_LENGTH);

        RuleFor(x => x.Score)
            .NotEmpty().NotNull();

        RuleFor(x => x.UserId).NotEmpty().NotNull();
        RuleFor(x => x.ExerciseId).NotEmpty().NotNull();

    }
}
