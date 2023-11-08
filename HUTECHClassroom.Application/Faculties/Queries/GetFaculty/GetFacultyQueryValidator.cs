namespace HUTECHClassroom.Application.Faculties.Queries.GetFaculty;

public sealed class GetFacultyQueryValidator : AbstractValidator<GetFacultyQuery>
{
    public GetFacultyQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
