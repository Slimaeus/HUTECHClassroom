namespace HUTECHClassroom.Application.Common.Validators.Classs;

public class ClassExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Class>
{
    public ClassExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
