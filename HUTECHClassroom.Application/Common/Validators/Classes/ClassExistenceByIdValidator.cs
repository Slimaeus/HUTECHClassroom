namespace HUTECHClassroom.Application.Common.Validators.Classs;

public class ClassExistenceByIdValidator : EntityExistenceByIdValidator<Class>
{
    public ClassExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
