using HUTECHClassroom.Application.Common.Validators.Classrooms;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Exercises.Commands.CreateExercise;

public class CreateExerciseCommandValidator : AbstractValidator<CreateExerciseCommand>
{
    public CreateExerciseCommandValidator(ClassroomExistenceByNotNullIdValidator classroomIdValidator)
    {
        RuleFor(x => x.Title)
            .MaximumLength(ExerciseConstants.TITLE_MAX_LENGTH)
            .NotEmpty().NotNull();

        RuleFor(x => x.Instruction)
            .MaximumLength(ExerciseConstants.INSTRUCTION_MAX_LENGTH)
            .NotEmpty().NotNull();

        RuleFor(x => x.Link)
            .MaximumLength(CommonConstants.LINK_MAX_LENGTH);

        RuleFor(x => x.Deadline);

        RuleFor(x => x.Topic)
            .MaximumLength(ExerciseConstants.TOPIC_MAX_LENGTH);

        RuleFor(x => x.Criteria)
            .MaximumLength(ExerciseConstants.CRITERIA_MAX_LENGTH);

        RuleFor(x => x.ClassroomId)
            .NotEmpty().NotNull()
            .SetValidator(classroomIdValidator);
    }
}
