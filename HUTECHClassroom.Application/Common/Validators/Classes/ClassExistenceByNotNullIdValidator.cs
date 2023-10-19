namespace HUTECHClassroom.Application.Common.Validators.Classs;

public class ClassExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<string, string, Class>
{
    public ClassExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
