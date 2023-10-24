using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Classs.Commands.DeleteRangeClass;

public record DeleteRangeClassCommand(IList<string> Ids) : DeleteRangeCommand<string>(Ids);
public class DeleteRangeClassCommandHandler : DeleteRangeCommandHandler<string, Class, DeleteRangeClassCommand>
{
    public DeleteRangeClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
