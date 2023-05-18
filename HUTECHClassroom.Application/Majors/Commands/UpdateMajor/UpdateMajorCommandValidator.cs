using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Majors.Commands.UpdateMajor;

public class UpdateMajorCommandValidator : UpdateCommandValidator<string, UpdateMajorCommand>
{
    public UpdateMajorCommandValidator()
    {
        RuleFor(x => x.Title).MaximumLength(50);
        RuleFor(x => x.TotalCredits).GreaterThanOrEqualTo(0);
        RuleFor(x => x.NonComulativeCredits).GreaterThanOrEqualTo(0);
    }
}
