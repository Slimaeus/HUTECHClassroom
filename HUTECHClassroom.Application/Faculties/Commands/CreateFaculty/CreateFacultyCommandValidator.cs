using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Faculties.Commands.CreateFaculty;

public sealed class CreateFacultyCommandValidator : AbstractValidator<CreateFacultyCommand>
{
    public CreateFacultyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(FacultyConstants.NAME_MAX_LENGTH);
    }
}
