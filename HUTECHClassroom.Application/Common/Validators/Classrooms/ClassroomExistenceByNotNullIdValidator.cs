namespace HUTECHClassroom.Application.Common.Validators.Classrooms;

public class ClassroomExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Classroom>
{
    public ClassroomExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
