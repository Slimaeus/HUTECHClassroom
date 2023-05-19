namespace HUTECHClassroom.Application.Majors.Commands.CreateMajor;

public class CreateMajorCommandValidator : AbstractValidator<CreateMajorCommand>
{
    public CreateMajorCommandValidator()
    {
        RuleFor(x => x.Code).NotNull().NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
        RuleFor(x => x.TotalCredits).GreaterThanOrEqualTo(0);
        RuleFor(x => x.NonComulativeCredits).GreaterThanOrEqualTo(0);
    }
}
