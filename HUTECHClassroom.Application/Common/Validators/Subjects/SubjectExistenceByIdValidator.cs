namespace HUTECHClassroom.Application.Common.Validators.Subjects;

public sealed class SubjectExistenceByIdValidator : EntityExistenceByIdValidator<Subject>
{
    public SubjectExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
