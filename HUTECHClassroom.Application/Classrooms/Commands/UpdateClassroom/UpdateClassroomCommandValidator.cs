using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Classrooms.Commands.UpdateClassroom;

public class UpdateClassroomCommandValidator : UpdateCommandValidator<UpdateClassroomCommand>
{
    public UpdateClassroomCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(100);
        RuleFor(x => x.Topic).MaximumLength(100);
    }
}
