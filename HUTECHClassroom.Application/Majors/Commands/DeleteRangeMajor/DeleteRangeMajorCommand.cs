using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Majors.Commands.DeleteRangeMajor;

public record DeleteRangeMajorCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangeMajorCommandHandler : DeleteRangeCommandHandler<Major, DeleteRangeMajorCommand>
{
    public DeleteRangeMajorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
