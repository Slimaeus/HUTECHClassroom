namespace HUTECHClassroom.Application.Common.Validators.Faculties;

public sealed class FacultyExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Faculty>
{
    public FacultyExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
