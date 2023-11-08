namespace HUTECHClassroom.Application.Common.Validators.Projects;

public sealed class ProjectExistenceByNotNullIdValidator : EntityExistenceByNotNullIdValidator<Project>
{
    public ProjectExistenceByNotNullIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
