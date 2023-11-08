namespace HUTECHClassroom.Application.Common.Validators.Projects;

public sealed class ProjectExistenceByIdValidator : EntityExistenceByIdValidator<Project>
{
    public ProjectExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
