using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Projects.Commands.DeleteRangeProject;

public record DeleteRangeProjectCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangeProjectCommandHandler : DeleteRangeCommandHandler<Project, DeleteRangeProjectCommand>
{
    public DeleteRangeProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
