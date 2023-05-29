namespace HUTECHClassroom.Application.Common.Validators.Faculties;

public class FacultyExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Faculty>
{
    public FacultyExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
