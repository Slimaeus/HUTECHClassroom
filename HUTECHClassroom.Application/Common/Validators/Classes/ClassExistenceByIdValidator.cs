namespace HUTECHClassroom.Application.Common.Validators.Classs;

public class ClassExistenceByIdValidator : EntityExistenceByIdValidator<string, string, Class>
{
    public ClassExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
