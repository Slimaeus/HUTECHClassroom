namespace HUTECHClassroom.Application.Common.Validators.Classs;

public sealed class ClassExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Class>
{
    public ClassExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
