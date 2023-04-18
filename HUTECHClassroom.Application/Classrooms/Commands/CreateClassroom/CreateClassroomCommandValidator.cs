using FluentValidation;

namespace HUTECHClassroom.Application.Classrooms.Commands.CreateClassroom
{
    public class CreateClassroomCommandValidator : AbstractValidator<CreateClassroomCommand>
    {
        public CreateClassroomCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(100);
            RuleFor(x => x.Topic).MaximumLength(100);

            RuleFor(x => x.LecturerName).NotEmpty().NotNull();
        }
    }
}
