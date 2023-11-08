namespace HUTECHClassroom.Application.Common.Validators.ScoreTypes;

public sealed class ScoreTypeExistenceByIdValidator : EntityExistenceByIdValidator<int, int?, ScoreType>
{
    public ScoreTypeExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
