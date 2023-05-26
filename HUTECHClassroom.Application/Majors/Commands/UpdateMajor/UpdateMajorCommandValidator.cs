using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Majors.Commands.UpdateMajor;

public class UpdateMajorCommandValidator : UpdateCommandValidator<UpdateMajorCommand>
{
    public UpdateMajorCommandValidator()
    {
        RuleFor(x => x.Code).MaximumLength(MajorConstants.CODE_MAX_VALUE);
        RuleFor(x => x.Title).MaximumLength(MajorConstants.TITLE_MAX_LENGTH);
        RuleFor(x => x.TotalCredits).GreaterThanOrEqualTo(0);
        RuleFor(x => x.NonComulativeCredits).GreaterThanOrEqualTo(0);
    }
}
