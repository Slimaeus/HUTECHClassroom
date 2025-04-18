using HUTECHClassroom.Application.Common.Validators.Faculties;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Majors.Commands.CreateMajor;

public sealed class CreateMajorCommandValidator : AbstractValidator<CreateMajorCommand>
{
    public CreateMajorCommandValidator(
        FacultyExistenceByIdValidator facultyIdValidator)
    {
        RuleFor(x => x.Code).NotNull().NotEmpty().MaximumLength(MajorConstants.CODE_MAX_VALUE);
        RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(MajorConstants.TITLE_MAX_LENGTH);
        RuleFor(x => x.TotalCredits).GreaterThanOrEqualTo(0);
        RuleFor(x => x.NonComulativeCredits).GreaterThanOrEqualTo(0);
        RuleFor(x => x.FacultyId)
            .SetValidator(facultyIdValidator);
    }
}
