using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Faculties.Commands.DeleteRangeFaculty;

public record DeleteRangeFacultyCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangeFacultyCommandHandler : DeleteRangeCommandHandler<Faculty, DeleteRangeFacultyCommand>
{
    public DeleteRangeFacultyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
