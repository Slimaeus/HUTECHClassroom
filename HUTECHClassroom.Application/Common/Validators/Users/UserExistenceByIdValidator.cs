namespace HUTECHClassroom.Application.Common.Validators.Users;

public class UserExistenceByIdValidator : EntityExistenceByIdValidator<ApplicationUser>
{
    public UserExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork, "The specified User Id does not exist.")
    {
    }
}
