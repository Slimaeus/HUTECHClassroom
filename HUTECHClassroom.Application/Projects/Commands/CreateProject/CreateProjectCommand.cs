using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Projects.Commands.CreateProject;

public sealed record CreateProjectCommand : CreateCommand
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Guid GroupId { get; set; }
}
public sealed class CreateProjectCommandHandler : CreateCommandHandler<Project, CreateProjectCommand>
{

    public CreateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
