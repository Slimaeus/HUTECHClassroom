using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Projects.Commands.CreateProject;

public record CreateProjectCommand : CreateCommand
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid GroupId { get; set; }
}
public class CreateProjectCommandHandler : CreateCommandHandler<Project, CreateProjectCommand>
{

    public CreateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
