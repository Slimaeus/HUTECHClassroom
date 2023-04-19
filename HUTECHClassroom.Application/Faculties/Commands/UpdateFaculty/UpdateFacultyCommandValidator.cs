using FluentValidation;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Faculties.Commands.UpdateFaculty;

public class UpdateFacultyCommandValidator : UpdateCommandValidator<UpdateFacultyCommand>
{
    public UpdateFacultyCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(50);
    }
}
