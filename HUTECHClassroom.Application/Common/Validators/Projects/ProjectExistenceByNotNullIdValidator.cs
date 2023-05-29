namespace HUTECHClassroom.Application.Common.Validators.Projects;

public class ProjectExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Project>
{
    public ProjectExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
