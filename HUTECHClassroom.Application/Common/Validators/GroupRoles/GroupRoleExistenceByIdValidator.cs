namespace HUTECHClassroom.Application.Common.Validators.GroupRoles;

public class GroupRoleExistenceByIdValidator : EntityExistenceByIdValidator<GroupRole>
{
    public GroupRoleExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
