using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Exercises.Commands.UpdateGroup;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Exercises.Commands.UpdateExercise;

public sealed class UpdateExerciseCommandValidator : UpdateCommandValidator<UpdateExerciseCommand>
{
    public UpdateExerciseCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(ExerciseConstants.TITLE_MAX_LENGTH);

        RuleFor(x => x.Instruction)
            .MaximumLength(ExerciseConstants.INSTRUCTION_MAX_LENGTH);

        RuleFor(x => x.Link)
            .MaximumLength(CommonConstants.LINK_MAX_LENGTH);

        RuleFor(x => x.Topic)
            .MaximumLength(ExerciseConstants.TOPIC_MAX_LENGTH);

        RuleFor(x => x.Criteria)
            .MaximumLength(ExerciseConstants.CRITERIA_MAX_LENGTH);
    }
}
