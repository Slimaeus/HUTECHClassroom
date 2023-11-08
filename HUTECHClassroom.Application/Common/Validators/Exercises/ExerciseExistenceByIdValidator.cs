namespace HUTECHClassroom.Application.Common.Validators.Exercises;

public sealed class ExerciseExistenceByIdValidator : EntityExistenceByIdValidator<Exercise>
{
    public ExerciseExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
