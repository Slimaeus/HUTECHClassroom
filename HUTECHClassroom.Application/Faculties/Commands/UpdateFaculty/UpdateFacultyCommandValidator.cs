using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Faculties.Commands.UpdateFaculty;

public class UpdateFacultyCommandValidator : UpdateCommandValidator<UpdateFacultyCommand>
{
    public UpdateFacultyCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(FacultyConstants.NAME_MAX_LENGTH);
    }
}
