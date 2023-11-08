namespace HUTECHClassroom.Application.Common.Validators.Classs;

public sealed class ClassExistenceByIdValidator : EntityExistenceByIdValidator<Class>
{
    public ClassExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
