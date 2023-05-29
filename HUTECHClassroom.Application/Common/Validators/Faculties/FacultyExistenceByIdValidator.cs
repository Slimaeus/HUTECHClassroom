namespace HUTECHClassroom.Application.Common.Validators.Faculties;

public class FacultyExistenceByIdValidator : EntityExistenceByIdValidator<Faculty>
{
    public FacultyExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork, "The specified Faculty Id does not exist.")
    {
    }
}
