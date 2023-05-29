namespace HUTECHClassroom.Application.Common.Validators.Classrooms;

public class ClassroomExistenceByIdValidator : EntityExistenceByIdValidator<Classroom>
{
    public ClassroomExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork, "The specified Classroom Id does not exist.")
    {
    }
}
