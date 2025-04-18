using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Common.Validators.Faculties;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Majors.Commands.UpdateMajor;

public sealed class UpdateMajorCommandValidator : UpdateCommandValidator<UpdateMajorCommand>
{
    public UpdateMajorCommandValidator(
        FacultyExistenceByIdValidator facultyIdValidator)
    {
        RuleFor(x => x.Code).MaximumLength(MajorConstants.CODE_MAX_VALUE);
        RuleFor(x => x.Title).MaximumLength(MajorConstants.TITLE_MAX_LENGTH);
        RuleFor(x => x.TotalCredits).GreaterThanOrEqualTo(0);
        RuleFor(x => x.NonComulativeCredits).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FacultyId)
            .SetValidator(facultyIdValidator);
    }
}
