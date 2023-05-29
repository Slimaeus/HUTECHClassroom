namespace HUTECHClassroom.Application.Common.Validators.Exercises;

public class ExerciseExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Exercise>
{
    public ExerciseExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
