namespace HUTECHClassroom.Application.Common.Validators.Missions;

public sealed class MissionExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Mission>
{
    public MissionExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
