using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Projects.Commands.UpdateProject;

public sealed record UpdateProjectCommand(Guid Id) : UpdateCommand(Id)
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
public sealed class UpdateProjectCommandHandler : UpdateCommandHandler<Project, UpdateProjectCommand>
{
    public UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
