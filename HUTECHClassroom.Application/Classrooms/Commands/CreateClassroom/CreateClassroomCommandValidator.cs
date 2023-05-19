namespace HUTECHClassroom.Application.Classrooms.Commands.CreateClassroom;

public class CreateClassroomCommandValidator : AbstractValidator<CreateClassroomCommand>
{
    public CreateClassroomCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(100);
        RuleFor(x => x.Topic).MaximumLength(100);
        RuleFor(x => x.Room).MaximumLength(100);
        RuleFor(x => x.SchoolYear).MaximumLength(100);
        RuleFor(x => x.Topic).MaximumLength(100);
        RuleFor(x => x.StudyGroup).MaximumLength(100);
        RuleFor(x => x.PracticalStudyGroup).MaximumLength(100);
        RuleFor(x => x.Semester).IsInEnum();
        RuleFor(x => x.Type).IsInEnum();

        RuleFor(x => x.LecturerName).NotEmpty().NotNull();
    }
}
