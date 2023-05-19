using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Subjects.Commands.UpdateSubject;

public class UpdateSubjectCommandValidator : UpdateCommandValidator<UpdateSubjectCommand>
{
    public UpdateSubjectCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty().NotNull().MaximumLength(50);
        RuleFor(x => x.Title).MaximumLength(50);
        RuleFor(x => x.TotalCredits).GreaterThanOrEqualTo(0);
    }
}
