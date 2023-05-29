namespace HUTECHClassroom.Application.Common.Validators.Subjects;

public class SubjectExistenceByIdValidator : EntityExistenceByIdValidator<Subject>
{
    public SubjectExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
