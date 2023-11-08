namespace HUTECHClassroom.Application.Common.Validators.GroupRoles;

public sealed class GroupRoleExistenceByIdValidator : EntityExistenceByIdValidator<GroupRole>
{
    public GroupRoleExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
