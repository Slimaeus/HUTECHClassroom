using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;

namespace HUTECHClassroom.Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(Guid Id) : DeleteCommand<ProjectDTO>(Id);
public sealed class DeleteProjectCommandHandler : DeleteCommandHandler<Project, DeleteProjectCommand, ProjectDTO>
{
    public DeleteProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
