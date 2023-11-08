namespace HUTECHClassroom.Application.Common.Validators.Groups;

public sealed class GroupExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Group>
{
    public GroupExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
