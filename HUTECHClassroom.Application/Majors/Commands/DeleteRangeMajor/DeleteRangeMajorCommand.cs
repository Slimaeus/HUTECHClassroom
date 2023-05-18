using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Majors.Commands.DeleteRangeMajor;

public record DeleteRangeMajorCommand(IList<string> Ids) : DeleteRangeCommand<string>(Ids);
public class DeleteRangeMajorCommandHandler : DeleteRangeCommandHandler<string, Major, DeleteRangeMajorCommand>
{
    public DeleteRangeMajorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
