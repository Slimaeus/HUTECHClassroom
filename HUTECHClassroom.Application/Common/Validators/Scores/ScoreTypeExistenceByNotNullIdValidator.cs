namespace HUTECHClassroom.Application.Common.Validators.ScoreTypes;

public class ScoreTypeExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<int, int, ScoreType>
{
    public ScoreTypeExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
