using FluentValidation;

namespace HUTECHClassroom.Application.Answers.Commands.CreateAnswer;

public class CreateAnswerCommandValidator : AbstractValidator<CreateAnswerCommand>
{
    public CreateAnswerCommandValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .NotEmpty().NotNull();

        RuleFor(x => x.Link)
            .MaximumLength(200);

        RuleFor(x => x.Score)
            .NotEmpty().NotNull();

        RuleFor(x => x.UserName).NotEmpty().NotNull();
        RuleFor(x => x.ExerciseId).NotEmpty().NotNull();
    }
}
