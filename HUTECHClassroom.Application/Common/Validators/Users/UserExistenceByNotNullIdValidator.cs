namespace HUTECHClassroom.Application.Common.Validators.Users;

public sealed class UserExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<ApplicationUser>
{
    public UserExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
