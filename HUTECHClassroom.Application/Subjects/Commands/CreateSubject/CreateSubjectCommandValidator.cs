namespace HUTECHClassroom.Application.Subjects.Commands.CreateSubject;

public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
{
    public CreateSubjectCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
        RuleFor(x => x.TotalCredits).GreaterThanOrEqualTo(0);
    }
}
