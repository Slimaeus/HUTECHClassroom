namespace HUTECHClassroom.Application.Common.Validators.Projects;

public class ProjectExistenceByIdValidator : EntityExistenceByIdValidator<Project>
{
    public ProjectExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
