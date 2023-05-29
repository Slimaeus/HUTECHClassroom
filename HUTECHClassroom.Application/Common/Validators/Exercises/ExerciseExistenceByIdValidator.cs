namespace HUTECHClassroom.Application.Common.Validators.Exercises;

public class ExerciseExistenceByIdValidator : EntityExistenceByIdValidator<Exercise>
{
    public ExerciseExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
