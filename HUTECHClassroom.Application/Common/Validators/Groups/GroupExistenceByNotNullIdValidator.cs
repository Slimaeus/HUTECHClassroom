namespace HUTECHClassroom.Application.Common.Validators.Groups;

public class GroupExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Group>
{
    public GroupExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
