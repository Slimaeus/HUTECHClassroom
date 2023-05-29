namespace HUTECHClassroom.Application.Common.Validators.Missions;

public class MissionExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Mission>
{
    public MissionExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
