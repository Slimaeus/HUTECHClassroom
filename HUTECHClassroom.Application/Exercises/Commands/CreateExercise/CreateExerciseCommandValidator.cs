using FluentValidation;

namespace HUTECHClassroom.Application.Exercises.Commands.CreateExercise;

public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
{
    public CreateExerciseCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(50)
            .NotEmpty().NotNull();

        RuleFor(x => x.Instruction)
            .MaximumLength(500)
            .NotEmpty().NotNull();

        RuleFor(x => x.Link)
            .MaximumLength(200);

        RuleFor(x => x.TotalScore)
            .NotEmpty().NotNull();

        RuleFor(x => x.Deadline);

        RuleFor(x => x.Topic)
            .MaximumLength(20);

        RuleFor(x => x.Criteria)
            .MaximumLength(200);

        RuleFor(x => x.ClassroomId).NotEmpty().NotNull();
    }
}
