namespace HUTECHClassroom.Application.Common.Validators.Exercises;

public sealed class ExerciseExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Exercise>
{
    public ExerciseExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
