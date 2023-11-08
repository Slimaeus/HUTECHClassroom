namespace HUTECHClassroom.Application.Common.Validators.ScoreTypes;

public sealed class ScoreTypeExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<int, int, ScoreType>
{
    public ScoreTypeExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
