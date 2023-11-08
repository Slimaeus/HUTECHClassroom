namespace HUTECHClassroom.Application.Common.Validators.Classrooms;

public sealed class ClassroomExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Classroom>
{
    public ClassroomExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
