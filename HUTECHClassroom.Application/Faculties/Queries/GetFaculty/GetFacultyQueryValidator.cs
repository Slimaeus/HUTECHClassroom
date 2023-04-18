using FluentValidation;

namespace HUTECHClassroom.Application.Faculties.Queries.GetFaculty;

public class GetFacultyQueryValidator : AbstractValidator<GetFacultyQuery>
{
    public GetFacultyQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
