namespace HUTECHClassroom.Application.Common.Validators.Faculties;

public sealed class FacultyExistenceByIdValidator : EntityExistenceByIdValidator<Faculty>
{
    public FacultyExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
