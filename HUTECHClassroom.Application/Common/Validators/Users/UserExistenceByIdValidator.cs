namespace HUTECHClassroom.Application.Common.Validators.Users;

public sealed class UserExistenceByIdValidator : EntityExistenceByIdValidator<ApplicationUser>
{
    public UserExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
