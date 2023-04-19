using FluentValidation;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Exercises.Commands.UpdateGroup;

public class UpdateExerciseCommandValidator : UpdateCommandValidator<UpdateExerciseCommand>
{
    public UpdateExerciseCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(50);

        RuleFor(x => x.Instruction)
            .MaximumLength(500);

        RuleFor(x => x.Link)
            .MaximumLength(200);

        RuleFor(x => x.TotalScore);

        RuleFor(x => x.Deadline);

        RuleFor(x => x.Topic)
            .MaximumLength(20);

        RuleFor(x => x.Criteria)
            .MaximumLength(200);
    }
}
