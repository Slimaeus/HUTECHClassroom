namespace HUTECHClassroom.Application.Common.Validators.Users;

public class UserExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<ApplicationUser>
{
    public UserExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
